import { Component, Input, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject } from 'rxjs';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { EditorAction } from 'src/app/shared/dtos/ModalEntityData';
import { CreatureType } from 'src/app/shared/models/creatures/CreatureType';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { CreatureTemplateModel, SkillTemplateModel } from 'src/app/shared/models/pocket/creature-templates/creature-template.model';
import { PocketCreature } from 'src/app/shared/models/pocket/creatures/pocket-creature';
import { SubscriptionManager } from 'src/app/shared/utils/subscription-manager';
import { PocketCreatureEditorComponent } from '../../pocket-creature-editor/pocket-creature-editor.component';
import { RollInput } from '../models/RollInput';
import { SimulateCdInput } from '../models/SimulateCdInput';
import { TakeDamageInput } from '../models/TakeDamangeInput';
import { PocketCampaignDetailsService } from '../pocket-campaign-bodyshell/pocket-campaign-details.service';
import { PocketCampaignsService } from '../pocket-campaigns.service';
import { RollOrigin } from '../campaign-heroes/RollOrigin';

@Component({
  selector: 'rr-campaign-creature-row',
  templateUrl: './campaign-creature-row.component.html',
  styleUrls: ['./campaign-creature-row.component.scss']
})
export class CampaignCreatureRowComponent implements OnInit {
  @Input() public creature: PocketCreature;
  public get isMaster() {
    return this.campaign.masterId === this.authService.userId;
  }
  public scene: CampaignScene = new CampaignScene();
  public rollOptions: MenuItem[] = [];
  public simluateCdOptions: MenuItem[] = [];
  public displayRollSidebar = false;
  public displaySimulateCdSidebar = false;
  public displayTakeDamageSidebar = false;
  public rollInputEmitter = new Subject<RollInput>();
  public simulateCdInputEmitter = new Subject<SimulateCdInput>();
  public rollResultEmitter = new Subject<boolean>();
  public simulateCdResultEmitter = new Subject<boolean>();
  public takeDamageInputEmitter = new Subject<TakeDamageInput>();
  public campaign: PocketCampaignModel = new PocketCampaignModel();
  private selectedCreatureForRoll: PocketCreature;
  private selectedCreatureForSimulateCd: PocketCreature;
  private subscriptionManager = new SubscriptionManager();

  private get creatureTemplate(): CreatureTemplateModel {
    return this.campaign.creatureTemplate;
  }
  constructor(
    private readonly campaignService: PocketCampaignsService,
    private readonly detailsService: PocketCampaignDetailsService,
    private authService: AuthenticationService,
    private readonly dialogService: DialogService,
  ) {
    this.subscribeToSceneChanges();
    this.subscribeToCampaignLoaded();
    this.subscribeToRollResult();
    this.subscribeToSimulateCdResult();
   }

   public isOwner(creature: PocketCreature) {
    return this.authService.userId === creature.ownerId;
   }

   public editCreature(creature: PocketCreature) {
     if (this.isOwner(creature) || this.isMaster) {
      this.dialogService.open(PocketCreatureEditorComponent, {
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
   public removeCreature(creature: PocketCreature) {
    this.campaignService.removeCreatureFromScene(this.campaign.id, this.scene.id, creature.id).subscribe(() => {
      if (creature.creatureType === CreatureType.Hero) {
        this.detailsService.heroRemovedToScene.next();
      } else {
        this.detailsService.monsterRemovedToScene.next();
      }
    });
   }
   public selectCreatureForRoll(creature: PocketCreature) {
    this.selectedCreatureForRoll = creature;
   }
     public selectCreatureForSimulateCd(creature: PocketCreature) {
    this.selectedCreatureForSimulateCd = creature;
   }

  private subscribeToCampaignLoaded() {
    this.subscriptionManager.add('campaignLoaded', this.detailsService.campaignLoaded.subscribe((campaign: PocketCampaignModel) => {
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
      const skills = this.creatureTemplate.skills.filter(skill => skill.attributeId === attribute.id) as SkillTemplateModel[];
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
      const skills = this.creatureTemplate.skills.filter(skill => skill.attributeId === attribute.id) as SkillTemplateModel[];
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
  private roll(creature: PocketCreature, propertyType: RollOrigin, propertyId: string) {
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
  private simulateCd(creature: PocketCreature, propertyType: RollOrigin, propertyId: string) {
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
  public takeDamage(creature: PocketCreature) {
    const input = {
      creature: creature
    } as TakeDamageInput
    this.displayTakeDamageSidebar = true;
    this.takeDamageInputEmitter.next(input);
  }
  ngOnInit(): void {
  }
  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }
}
