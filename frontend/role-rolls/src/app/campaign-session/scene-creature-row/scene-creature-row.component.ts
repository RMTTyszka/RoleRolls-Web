import { Component, Input, signal } from '@angular/core';
import { CampaignScene } from '@app/campaigns/models/campaign-scene-model';
import { MenuItem, PrimeTemplate } from 'primeng/api';
import { Subject } from 'rxjs';
import { SubscriptionManager } from '@app/tokens/subscription-manager';
import { AuthenticationService } from '@app/authentication/services/authentication.service';
import { DialogService, DynamicDialogModule } from 'primeng/dynamicdialog';
import { EditorAction } from '@app/models/EntityActionData';
import { RollOrigin } from '@app/models/RollOrigin';
import { RollInput } from '@app/campaigns/models/RollInput';
import { SimulateCdInput } from '@app/campaigns/models/SimulateCdInput';
import { TakeDamageInput } from '@app/campaigns/models/TakeDamangeInput';
import { Creature } from '@app/campaigns/models/creature';
import { Campaign } from '@app/campaigns/models/campaign';
import { CampaignsService } from '@app/campaigns/services/campaigns.service';
import { CampaignTemplate, SkillTemplate } from '@app/campaigns/models/campaign.template';
import { CampaignSessionService } from '@app/campaign-session/campaign-session.service';
import { CreatureEditorComponent } from '@app/creatures/creature-editor/creature-editor.component';
import { CreatureCategory } from '@app/campaigns/models/CreatureCategory';
import { ButtonDirective, ButtonModule } from 'primeng/button';
import {NgForOf, NgIf} from '@angular/common';
import { Popover, PopoverModule } from 'primeng/popover';
import { TieredMenu, TieredMenuModule } from 'primeng/tieredmenu';
import { Tooltip, TooltipModule } from 'primeng/tooltip';
import { Sidebar, SidebarModule } from 'primeng/sidebar';
import { RollDiceComponent } from '@app/campaign-session/scene-rolls/roll-dice/roll-dice.component';
import { SimulateCdComponent } from '@app/campaign-session/scene-rolls/simulate-cd/simulate-cd.component';
import { AttackComponent } from '@app/campaign-session/creature-actions/attack/attack.component';
import {
  VitalityManagerComponent
} from '@app/campaign-session/creature-actions/vitality-manager/vitality-manager.component';
import { Drawer, DrawerModule } from 'primeng/drawer';

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
    SidebarModule,
    DrawerModule,
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
        }
      });
    }

  }
  public removeCreature(creature: Creature) {
    this.campaignService.removeCreatureFromScene(this.campaign.id, this.scene.id, creature.id).subscribe(() => {
      if (creature.category === CreatureCategory.Hero) {
        this.detailsService.heroRemovedToScene.next();
      } else {
        this.detailsService.monsterRemovedToScene.next();
      }
    });
  }
  public selectCreatureForRoll(creature: Creature) {
    this.selectedCreatureForRoll = creature;
  }
  public selectCreatureForSimulateCd(creature: Creature) {
    this.selectedCreatureForSimulateCd = creature;
  }

  private subscribeToCampaignLoaded() {
    this.subscriptionManager.add('campaignLoaded', this.detailsService.campaignLoaded.subscribe((campaign: Campaign) => {
      if (campaign) {
        this.campaign = campaign;
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
    this.creatureTemplate.attributes.forEach(attribute => {
      const attributeMenu = {
        label: attribute.name,
        items: [
          {
            label: `Roll ${attribute.name}`,
            command: (event) => {
              this.roll(this.selectedCreatureForRoll, RollOrigin.Attribute, attribute.id);
            }
          } as MenuItem
        ]
      } as MenuItem;
      const skills = this.creatureTemplate.skills.filter(skill => skill.attributeId === attribute.id) as SkillTemplate[];
      skills.forEach(skill => {
        const skillMenu = {
          label: skill.name,
          items: [
            {
              label: `Roll ${skill.name}`,
              command: (event) => {
                this.roll(this.selectedCreatureForRoll, RollOrigin.Skill, skill.id);
              }
            } as MenuItem
          ]
        } as MenuItem;
        skill.minorSkills.forEach(minorSkill => {
          const minorSkillMenu = {
            label: minorSkill.name,
            command: (event) => {
              this.roll(this.selectedCreatureForRoll, RollOrigin.MinorSkill, minorSkill.id);
            }
          } as MenuItem;
          skillMenu.items.push(minorSkillMenu);
        });
        attributeMenu.items.push(skillMenu);
      });
      this.rollOptions.push(attributeMenu);
    });
  }
  private populateSimulateCdOptions() {
    this.creatureTemplate.attributes.forEach(attribute => {
      const attributeMenu = {
        label: attribute.name,
        items: [
          {
            label: `Roll ${attribute.name}`,
            command: (event) => {
              this.simulateCd(this.selectedCreatureForSimulateCd, RollOrigin.Attribute, attribute.id);
            }
          } as MenuItem
        ]
      } as MenuItem;
      const skills = this.creatureTemplate.skills.filter(skill => skill.attributeId === attribute.id) as SkillTemplate[];
      skills.forEach(skill => {
        const skillMenu = {
          label: skill.name,
          items: [
            {
              label: `Roll ${skill.name}`,
              command: (event) => {
                this.simulateCd(this.selectedCreatureForSimulateCd, RollOrigin.Skill, skill.id);
              }
            } as MenuItem
          ]
        } as MenuItem;
        skill.minorSkills.forEach(minorSkill => {
          const minorSkillMenu = {
            label: minorSkill.name,
            command: (event) => {
              this.simulateCd(this.selectedCreatureForSimulateCd, RollOrigin.MinorSkill, minorSkill.id);
            }
          } as MenuItem;
          skillMenu.items.push(minorSkillMenu);
        });
        attributeMenu.items.push(skillMenu);
      });
      this.simluateCdOptions.push(attributeMenu);
    });
  }
  private roll(creature: Creature, propertyType: RollOrigin, propertyId: string) {
    const input = {
      propertyType: propertyType,
      creatureId: creature.id,
    } as RollInput;
    if (propertyType === RollOrigin.Attribute) {
      const attribute = creature.attributes.find(a => a.attributeTemplateId === propertyId);
      input.propertyId = attribute.id;
      input.propertyName = attribute.name;
      input.propertyValue = attribute.value;
    } else if (propertyType === RollOrigin.Skill) {
      const skill = creature.skills.find(s => s.skillTemplateId === propertyId);
      input.propertyId = skill.id;
      input.propertyName = skill.name;
      input.propertyValue = skill.value;
    } else if (propertyType === RollOrigin.MinorSkill) {
      const skill = creature.skills.find(s => s.minorSkills.some(m => m.minorSkillTemplateId === propertyId));
      const minorSkills = skill.minorSkills.find(m => m.minorSkillTemplateId === propertyId);
      input.propertyId = minorSkills.id;
      input.propertyName = minorSkills.name;
      input.propertyValue = skill.value;
    }
    this.displayRollSidebar = true;
    this.rollInputEmitter.next(input);
  }
  private simulateCd(creature: Creature, propertyType: RollOrigin, propertyId: string) {
    const input = {
      propertyType: propertyType,
      creatureId: creature.id,
    } as SimulateCdInput;
    if (propertyType === RollOrigin.Attribute) {
      const attribute = creature.attributes.find(a => a.attributeTemplateId === propertyId);
      input.propertyId = attribute.id;
    } else if (propertyType === RollOrigin.Skill) {
      const skill = creature.skills.find(s => s.skillTemplateId === propertyId);
      input.propertyId = skill.id;
    } else if (propertyType === RollOrigin.MinorSkill) {
      const skill = creature.skills.find(s => s.minorSkills.some(m => m.minorSkillTemplateId === propertyId));
      const minorSkills = skill.minorSkills.find(m => m.minorSkillTemplateId === propertyId);
      input.propertyId = minorSkills.id;
    }
    this.displaySimulateCdSidebar = true;
    this.simulateCdInputEmitter.next(input);
  }
  public takeDamage(creature: Creature) {
    const input = {
      creature: creature
    } as TakeDamageInput;
    this.displayTakeDamageSidebar = true;
    this.takeDamageInputEmitter.next(input);
  }
  ngOnInit(): void {
  }
  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }

  public attack(creature: Creature) {
    this.attacker.set(creature);
    this.displayAttackSidebar.set(true);
  }
}
