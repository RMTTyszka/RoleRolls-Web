import { Component, OnDestroy, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Subject } from 'rxjs';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { CreatureType } from 'src/app/shared/models/creatures/CreatureType';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { CreatureTemplateModel, SkillTemplateModel } from 'src/app/shared/models/pocket/creature-templates/creature-template.model';
import { PocketCreature, PocketMonster } from 'src/app/shared/models/pocket/creatures/pocket-creature';
import { SubscriptionManager } from 'src/app/shared/utils/subscription-manager';
import { RollOrigin } from '../campaign-heroes/RollOrigin';
import { RollInput } from '../models/RollInput';
import { PocketCampaignDetailsService } from '../pocket-campaign-bodyshell/pocket-campaign-details.service';
import { PocketCampaignsService } from '../pocket-campaigns.service';

@Component({
  selector: 'rr-campaign-monsters',
  templateUrl: './campaign-monsters.component.html',
  styleUrls: ['./campaign-monsters.component.scss']
})
export class CampaignMonstersComponent implements OnInit, OnDestroy {
  public monsters: PocketMonster[] = [];

  public get isMaster() {
    return this.campaign.masterId === this.authService.userId;
  }
  public rollOptions: MenuItem[] = [];
  public displayRollSidebar = false;
  public rollInputEmitter = new Subject<RollInput>();
  public scene: CampaignScene = new CampaignScene();
  public campaign: PocketCampaignModel = new PocketCampaignModel();
  private selectedMonsterForRoll: PocketMonster;
  private subscriptionManager = new SubscriptionManager();

  private get creatureTemplate(): CreatureTemplateModel {
    return this.campaign.creatureTemplate;
  }
  constructor(
    private readonly campaignService: PocketCampaignsService,
    private readonly detailsService: PocketCampaignDetailsService,
    private authService: AuthenticationService,
  ) {
    this.subscribeToCampaignLoaded();
    this.subscribeToSceneChanges();
   // this.subscribeToMonsterAdded();
   }

   public removeMonster(monster: PocketMonster) {
    this.campaignService.removeCreatureFromScene(this.campaign.id, this.scene.id, monster.id).subscribe(() => {
      this.refreshMonsteres();
    });
   }
   public selectMonsterForRoll(monster: PocketMonster) {
    this.selectedMonsterForRoll = monster;
   }

  private subscribeToCampaignLoaded() {
    this.subscriptionManager.add('campaignLoaded', this.detailsService.campaignLoaded.subscribe((campaign: PocketCampaignModel) => {
        this.campaign = campaign;
        this.populateRollOptions();
    }));
  }

  private subscribeToSceneChanges() {
    this.subscriptionManager.add('sceneChanged', this.detailsService.sceneChanged.subscribe((scene: CampaignScene) => {
        this.scene = scene;
        this.refreshMonsteres();
    }));
  }

/*   private subscribeToMonsterAdded() {
    this.subscriptionManager.add('monsterAddedToScene', this.detailsService.monsterAddedToScene.subscribe(() => {
        this.refreshMonsteres();
    }));
  } */
  private refreshMonsteres() {
    this.campaignService.getSceneCreatures(this.scene.campaignId, this.scene.id, CreatureType.Monster)
    .subscribe((monsteres: PocketCreature[]) => {
      this.monsters = monsteres as PocketMonster[];
    });
  }

  private populateRollOptions() {
    this.creatureTemplate.attributes.forEach(attribute => {
      const attributeMenu = {
        label: attribute.name,
        items: [
          {
            label: `Roll ${attribute.name}`,
            command: (event) => {
              this.roll(this.selectedMonsterForRoll, RollOrigin.Attribute, attribute.id);
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
                this.roll(this.selectedMonsterForRoll, RollOrigin.Skill, skill.id);
              }
            } as MenuItem
          ]
        } as MenuItem;
        skill.minorSkills.forEach(minorSkill => {
          const minorSkillMenu = {
            label: minorSkill.name,
            command: (event) => {
              this.roll(this.selectedMonsterForRoll, RollOrigin.MinorSkill, minorSkill.id);
            }
          } as MenuItem;
          skillMenu.items.push(minorSkillMenu);
        });
        attributeMenu.items.push(skillMenu);
      });
      this.rollOptions.push(attributeMenu);
    });
  }
  private roll(monster: PocketMonster, propertyType: RollOrigin, propertyId: string) {
    const input = {
      propertyType: propertyType,
      creatureId: monster.id,
    } as RollInput;
    if (propertyType === RollOrigin.Attribute) {
      const attribute = monster.attributes.find(a => a.attributeTemplateId === propertyId);
      input.propertyId = attribute.id;
      input.propertyName = attribute.name;
      input.propertyValue = attribute.value;
    } else if (propertyType === RollOrigin.Skill) {
      const skill = monster.skills.find(s => s.skillTemplateId === propertyId);
      input.propertyId = skill.id;
      input.propertyName = skill.name;
      input.propertyValue = skill.value;
    } else if (propertyType === RollOrigin.MinorSkill) {
      const skill = monster.skills.find(s => s.minorSkills.some(m => m.minorSkillTemplateId === propertyId));
      const minorSkills = skill.minorSkills.find(m => m.minorSkillTemplateId === propertyId);
      input.propertyId = skill.id;
      input.propertyName = minorSkills.name;
      input.propertyValue = skill.value;
    }
    this.displayRollSidebar = true;
    this.rollInputEmitter.next(input);
  }
  ngOnInit(): void {
  }
  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }
}
