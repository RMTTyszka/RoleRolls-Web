import { Component, Input, signal } from '@angular/core';
import { CampaignScene } from '@app/campaigns/models/campaign-scene-model';
import { MenuItem } from 'primeng/api';
import { Subject } from 'rxjs';
import { SubscriptionManager } from '@app/tokens/subscription-manager';
import { AuthenticationService } from '@app/authentication/services/authentication.service';
import { DialogService } from 'primeng/dynamicdialog';
import { EditorAction } from '@app/models/EntityActionData';
import { RollInput } from '@app/campaigns/models/RollInput';
import { SimulateCdInput } from '@app/campaigns/models/SimulateCdInput';
import { TakeDamageInput } from '@app/campaigns/models/TakeDamangeInput';
import { Creature } from '@app/campaigns/models/creature';
import { Campaign } from '@app/campaigns/models/campaign';
import { CampaignsService } from '@app/campaigns/services/campaigns.service';
import {
  AttributeTemplate,
  CampaignTemplate,
  SkillTemplate,
  SpecificSkillsTemplate
} from '@app/campaigns/models/campaign.template';
import { CampaignSessionService } from '@app/campaign-session/campaign-session.service';
import { CreatureEditorComponent } from '@app/creatures/creature-editor/creature-editor.component';
import { CreatureCategory } from '@app/campaigns/models/CreatureCategory';
import { ButtonDirective } from 'primeng/button';
import {NgForOf, NgIf} from '@angular/common';
import { PopoverModule } from 'primeng/popover';
import { TieredMenu } from 'primeng/tieredmenu';
import { Tooltip } from 'primeng/tooltip';
import { RollDiceComponent } from '@app/campaign-session/scene-rolls/roll-dice/roll-dice.component';
import { SimulateCdComponent } from '@app/campaign-session/scene-rolls/simulate-cd/simulate-cd.component';
import { AttackComponent } from '@app/campaign-session/creature-actions/attack/attack.component';
import {
  VitalityManagerComponent
} from '@app/campaign-session/creature-actions/vitality-manager/vitality-manager.component';
import { Drawer, DrawerModule } from 'primeng/drawer';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { Property } from '@app/models/bonuses/bonus';
import { PropertyType } from '@app/campaigns/models/propertyType';

@Component({
  selector: 'rr-scene-creature-row',
  imports: [
    ButtonDirective,
    NgIf,
    PopoverModule,
    TieredMenu,
    Tooltip,
    RollDiceComponent,
    SimulateCdComponent,
    AttackComponent,
    VitalityManagerComponent,
    NgForOf,
    DrawerModule,
    InputGroupAddonModule,
  ],
  templateUrl: './scene-creature-row.component.html',
  styleUrl: './scene-creature-row.component.scss'
})
export class SceneCreatureRowComponent {
  @Input() public creature: Creature;
  public get isMaster() {
    return this.campaign.masterId === this.authService.userId;
  }
  public scene: CampaignScene;
  public rollOptions: MenuItem[] = [];
  public simluateCdOptions: MenuItem[] = [];
  public displayRollSidebar = false;
  public displaySimulateCdSidebar = false;
  public displayTakeDamageSidebar = false;
  public rollInputEmitter = new Subject<RollInput>();
  public simulateCdInputEmitter = new Subject<SimulateCdInput>();
  public attacker = signal<Creature>(null);
  public rollResultEmitter = new Subject<boolean>();
  public simulateCdResultEmitter = new Subject<boolean>();
  public displayAttackSidebar = signal<boolean>(false);
  public takeDamageInputEmitter = new Subject<TakeDamageInput>();
  public campaign: Campaign;
  private selectedCreatureForRoll: Creature;
  private selectedCreatureForSimulateCd: Creature;
  private subscriptionManager = new SubscriptionManager();

  private get creatureTemplate(): CampaignTemplate {
    return this.campaign.campaignTemplate;
  }
  constructor(
    private readonly campaignService: CampaignsService,
    private readonly detailsService: CampaignSessionService,
    private authService: AuthenticationService,
    private readonly dialogService: DialogService,
  ) {
    this.subscribeToSceneChanges();
    this.subscribeToCampaignLoaded();
    this.subscribeToRollResult();
    this.subscribeToSimulateCdResult();
  }

  public isOwner(creature: Creature) {
    return this.authService.userId === creature.ownerId;
  }

  public editCreature(creature: Creature) {
    if (this.isOwner(creature) || this.isMaster) {
      this.dialogService.open(CreatureEditorComponent, {
        width: '100vw',
        height: '100vh',
        data: {
          campaign: this.campaign,
          action: EditorAction.update,
          creatureId: creature.id
        },
        closable: true,
      });
    }

  }
  public removeCreature(creature: Creature) {
    this.campaignService.removeCreatureFromScene(this.campaign.id, this.scene.id, creature.id).subscribe(() => {
      if (creature.category === CreatureCategory.Ally) {
        this.detailsService.heroRemovedToScene.next();
      } else {
        this.detailsService.monsterRemovedToScene.next();
      }
    });
  }
  public addTokenToBoard(creature: Creature) {
    if (!this.scene) {
      return;
    }

    this.detailsService.creatureTokenRequested.next({
      creature,
      sceneId: this.scene.id
    });
  }
  public selectCreatureForRoll(creature: Creature) {
    this.selectedCreatureForRoll = creature;
    this.displayRollSidebar = false;
  }
  public quickRoll(creature: Creature) {
    this.selectedCreatureForRoll = creature;
    this.displayRollSidebar = false;
    this.roll(this.selectedCreatureForRoll, null, null);
  }
  public selectCreatureForSimulateCd(creature: Creature) {
    this.selectedCreatureForSimulateCd = creature;
    this.displaySimulateCdSidebar = false;
  }

  private subscribeToCampaignLoaded() {
    this.subscriptionManager.add('campaignLoaded', this.detailsService.campaignLoaded.subscribe((campaign: Campaign) => {
      if (campaign) {
        this.campaign = campaign;
        this.rollOptions = [];
        this.simluateCdOptions = [];
        this.populateRollOptions();
        this.populateSimulateCdOptions();
      }
    }));
  }
  private subscribeToSceneChanges() {
    this.subscriptionManager.add('sceneChanged', this.detailsService.sceneChanged.subscribe((scene: CampaignScene) => {
      this.scene = scene;
    }));
  }


  private subscribeToRollResult() {
    this.subscriptionManager.add('rollResultEmitter', this.rollResultEmitter.subscribe(() => {
      this.displayRollSidebar = false;
    }));
  }
  private subscribeToSimulateCdResult() {
    this.subscriptionManager.add('simulateCdResultEmitter', this.simulateCdResultEmitter.subscribe(() => {
      this.displayRollSidebar = false;
    }));
  }


  private populateRollOptions() {
    this.rollOptions = this.buildAttributeMenus((propertyType, propertyId, attributeId) => {
      this.roll(this.selectedCreatureForRoll, propertyType, propertyId, attributeId);
    });
  }
  private populateSimulateCdOptions() {
    this.simluateCdOptions = this.buildAttributeMenus((propertyType, propertyId) => {
      this.simulateCd(this.selectedCreatureForSimulateCd, propertyType, propertyId);
    });
  }
  private buildAttributeMenus(
    commandFactory: (propertyType: PropertyType, propertyId: string, attributeId?: string | null) => void
  ): MenuItem[] {
    return this.creatureTemplate.attributes
      .map(attribute => this.buildAttributeMenu(attribute, commandFactory))
      .filter((menu): menu is MenuItem => menu !== null);
  }

  private buildAttributeMenu(
    attribute: AttributeTemplate,
    commandFactory: (propertyType: PropertyType, propertyId: string, attributeId?: string | null) => void
  ): MenuItem | null {
    const items: MenuItem[] = [{
      label: `Roll ${attribute.name}`,
      command: () => {
        commandFactory(PropertyType.Attribute, attribute.id, attribute.id);
      }
    }];

    for (const skill of this.creatureTemplate.skills) {
      const skillMenu = this.buildSkillMenuForAttribute(skill, attribute.id, commandFactory);
      if (skillMenu) {
        items.push(skillMenu);
      }
    }

    if (items.length === 0) {
      return null;
    }

    return {
      label: attribute.name,
      items
    };
  }

  private buildSkillMenuForAttribute(
    skill: SkillTemplate,
    attributeId: string,
    commandFactory: (propertyType: PropertyType, propertyId: string, attributeId?: string | null) => void
  ): MenuItem | null {
    const specificSkills = skill.specificSkillTemplates.filter(specificSkill =>
      this.resolveSpecificSkillTemplateAttributeId(skill, specificSkill) === attributeId
    );

    if (skill.attributeId !== attributeId && specificSkills.length === 0) {
      return null;
    }

    const items: MenuItem[] = [{
      label: `Roll ${skill.name}`,
      command: () => {
        commandFactory(PropertyType.Skill, skill.id, attributeId);
      }
    }];

    for (const specificSkill of specificSkills) {
      items.push({
        label: specificSkill.name,
        command: () => {
          commandFactory(PropertyType.SpecificSkill, specificSkill.id, attributeId);
        }
      });
    }

    return {
      label: skill.name,
      items
    };
  }

  private resolveSpecificSkillTemplateAttributeId(
    skill: SkillTemplate,
    specificSkill: SpecificSkillsTemplate
  ): string | null {
    return specificSkill.attributeTemplateId ?? skill.attributeId ?? null;
  }

  private roll(creature: Creature, propertyType: PropertyType, propertyId: string, attributeId: string | null = null) {
    const input =  {
      property:(propertyType !== null && propertyId !== null) ? { type: propertyType, id: propertyId } as Property : null,
      creature: creature,
    } as RollInput;
    if (propertyType === PropertyType.Attribute) {
      const attribute = creature.attributes.find(a => a.attributeTemplateId === propertyId);
      input.property.id = attribute.attributeTemplateId;
      input.propertyName = attribute.name;
      input.propertyValue = attribute.value;
    } else if (propertyType === PropertyType.Skill) {
      const skill = creature.skills.find(s => s.skillTemplateId === propertyId);
      input.property.id = skill.skillTemplateId;
      input.propertyName = skill.name;
      input.propertyValue = skill.value;
      if (attributeId) {
        input.attribute = {
          type: PropertyType.Attribute,
          id: attributeId
        } as Property;
      }
    } else if (propertyType === PropertyType.SpecificSkill) {
      const skill = creature.skills.find(s => s.specificSkills.some(m => m.specificSkillTemplateId === propertyId));
      const specificSkills = skill.specificSkills.find(m => m.specificSkillTemplateId === propertyId);
      input.property.id = specificSkills.specificSkillTemplateId;
      input.propertyName = specificSkills.name;
      input.propertyValue = skill.value;
    }
    this.displayRollSidebar = true;
    this.rollInputEmitter.next(input);
  }
  private simulateCd(creature: Creature, propertyType: PropertyType, propertyId: string) {
    const input = {
      property: {
        id: propertyId,
        type: propertyType,
      } as Property,
      creatureId: creature.id,
    } as SimulateCdInput;
    if (propertyType === PropertyType.Attribute) {
      const attribute = creature.attributes.find(a => a.attributeTemplateId === propertyId);
      input.property.id = attribute.attributeTemplateId;
    } else if (propertyType === PropertyType.Skill) {
      const skill = creature.skills.find(s => s.skillTemplateId === propertyId);
      input.property.id = skill.skillTemplateId;
    } else if (propertyType === PropertyType.SpecificSkill) {
      const skill = creature.skills.find(s => s.specificSkills.some(m => m.specificSkillTemplateId === propertyId));
      const specificSkills = skill.specificSkills.find(m => m.specificSkillTemplateId === propertyId);
      input.property.id = specificSkills.specificSkillTemplateId;
    }
    this.displaySimulateCdSidebar = !this.displaySimulateCdSidebar;
    this.simulateCdInputEmitter.next(input);
  }
  public takeDamage(creature: Creature) {
    const input = {
      creature: creature
    } as TakeDamageInput;
    this.displayTakeDamageSidebar = !this.displayTakeDamageSidebar;
    this.takeDamageInputEmitter.next(input);
  }
  ngOnInit(): void {
  }
  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }

  public attack(creature: Creature) {
    const hasCreature = Boolean(creature);
    this.displayAttackSidebar.set(hasCreature);
    setTimeout(() => {
      this.attacker.set(creature);
    })
  }
}
