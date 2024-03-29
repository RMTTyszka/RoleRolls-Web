import { SimulateCdInput } from './../models/SimulateCdInput';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Subject } from 'rxjs';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { CreatureType } from 'src/app/shared/models/creatures/CreatureType';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { CreatureTemplateModel, SkillTemplateModel } from 'src/app/shared/models/pocket/creature-templates/creature-template.model';
import { PocketCreature, PocketHero } from 'src/app/shared/models/pocket/creatures/pocket-creature';
import { SubscriptionManager } from 'src/app/shared/utils/subscription-manager';
import { RollInput } from '../models/RollInput';
import { PocketCampaignDetailsService } from '../pocket-campaign-bodyshell/pocket-campaign-details.service';
import { PocketCampaignsService } from '../pocket-campaigns.service';
import { RollOrigin } from './RollOrigin';
import { EditorAction } from 'src/app/shared/dtos/ModalEntityData';
import { PocketCreatureEditorComponent } from 'src/app/pocket-role-rolls/pocket-creature-editor/pocket-creature-editor.component';
import { DialogService } from 'primeng/dynamicdialog';
import { TakeDamageInput } from '../models/TakeDamangeInput';
import { OverlayPanel } from 'primeng/overlaypanel';

@Component({
  selector: 'rr-campaign-heroes',
  templateUrl: './campaign-heroes.component.html',
  styleUrls: ['./campaign-heroes.component.scss'],
  providers: [DialogService]
})
export class CampaignHeroesComponent implements OnInit, OnDestroy {
  public heroes: PocketHero[] = [];
  public get isMaster() {
    return this.campaign.masterId === this.authService.userId;
  }
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
  public scene: CampaignScene = new CampaignScene();
  public campaign: PocketCampaignModel = new PocketCampaignModel();
  private selectedHeroForRoll: PocketHero;
  private selectedHeroForSimulateCd: PocketHero;
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
    this.subscribeToCampaignLoaded();
    this.subscribeToSceneChanges();
    this.subscribeToHeroAdded();
    this.subscribeToRollResult();
    this.subscribeToSimulateCdResult();
    this.subscribeToHeroTookDamage();
   }

   public isOwner(hero: PocketHero) {
    return this.authService.userId === hero.ownerId;
   }

   public editHero(hero: PocketHero) {
     if (this.isOwner(hero) || this.isMaster) {
      this.dialogService.open(PocketCreatureEditorComponent, {
        width: '100vw',
        height: '100vh',
        data: {
          campaign: this.campaign,
          action: EditorAction.update,
          creatureId: hero.id
        }
      });
     }

   }
   public removeHero(hero: PocketHero) {
    this.campaignService.removeCreatureFromScene(this.campaign.id, this.scene.id, hero.id).subscribe(() => {
      this.refreshHeroes(false);
    });
   }
   public selectHeroForRoll(hero: PocketHero) {
    this.selectedHeroForRoll = hero;
   }
     public selectHeroForSimulateCd(hero: PocketHero) {
    this.selectedHeroForSimulateCd = hero;
   }

  private subscribeToCampaignLoaded() {
    this.subscriptionManager.add('campaignLoaded', this.detailsService.campaignLoaded.subscribe((campaign: PocketCampaignModel) => {
        this.campaign = campaign;
        this.populateRollOptions();
        this.populateSimulateCdOptions();
    }));
  }

  private subscribeToSceneChanges() {
    this.subscriptionManager.add('sceneChanged', this.detailsService.sceneChanged.subscribe((scene: CampaignScene) => {
        this.scene = scene;
        this.refreshHeroes(false);
    }));
  }

  private subscribeToHeroAdded() {
    this.subscriptionManager.add('heroAddedToScene', this.detailsService.heroAddedToScene.subscribe(() => {
        this.refreshHeroes(false);
    }));
  }

  private subscribeToHeroTookDamage() {
    this.subscriptionManager.add('heroTookDamage', this.detailsService.heroTookDamage.subscribe(() => {
        this.refreshHeroes(true);
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
  private refreshHeroes(keepObjects: boolean) {
    this.campaignService.getSceneCreatures(this.scene.campaignId, this.scene.id, CreatureType.Hero)
    .subscribe((heroes: PocketCreature[]) => {
      if (!keepObjects) {
        this.heroes = heroes as PocketHero[];
      } else {
        this.heroes.forEach(hero => {
          const updatedHero = heroes.filter(h => h.id == hero.id)[0];
          if (updatedHero) {
            Object.assign(hero, updatedHero)
          }
        });
      }
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
              this.roll(this.selectedHeroForRoll, RollOrigin.Attribute, attribute.id);
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
                this.roll(this.selectedHeroForRoll, RollOrigin.Skill, skill.id);
              }
            } as MenuItem
          ]
        } as MenuItem;
        skill.minorSkills.forEach(minorSkill => {
          const minorSkillMenu = {
            label: minorSkill.name,
            command: (event) => {
              this.roll(this.selectedHeroForRoll, RollOrigin.MinorSkill, minorSkill.id);
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
              this.simulateCd(this.selectedHeroForSimulateCd, RollOrigin.Attribute, attribute.id);
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
                this.simulateCd(this.selectedHeroForSimulateCd, RollOrigin.Skill, skill.id);
              }
            } as MenuItem
          ]
        } as MenuItem;
        skill.minorSkills.forEach(minorSkill => {
          const minorSkillMenu = {
            label: minorSkill.name,
            command: (event) => {
              this.simulateCd(this.selectedHeroForSimulateCd, RollOrigin.MinorSkill, minorSkill.id);
            }
          } as MenuItem;
          skillMenu.items.push(minorSkillMenu);
        });
        attributeMenu.items.push(skillMenu);
      });
      this.simluateCdOptions.push(attributeMenu);
    });
  }
  private roll(hero: PocketHero, propertyType: RollOrigin, propertyId: string) {
    const input = {
      propertyType: propertyType,
      creatureId: hero.id,
    } as RollInput;
    if (propertyType === RollOrigin.Attribute) {
      const attribute = hero.attributes.find(a => a.attributeTemplateId === propertyId);
      input.propertyId = attribute.id;
      input.propertyName = attribute.name;
      input.propertyValue = attribute.value;
    } else if (propertyType === RollOrigin.Skill) {
      const skill = hero.skills.find(s => s.skillTemplateId === propertyId);
      input.propertyId = skill.id;
      input.propertyName = skill.name;
      input.propertyValue = skill.value;
    } else if (propertyType === RollOrigin.MinorSkill) {
      const skill = hero.skills.find(s => s.minorSkills.some(m => m.minorSkillTemplateId === propertyId));
      const minorSkills = skill.minorSkills.find(m => m.minorSkillTemplateId === propertyId);
      input.propertyId = minorSkills.id;
      input.propertyName = minorSkills.name;
      input.propertyValue = skill.value;
    }
    this.displayRollSidebar = true;
    this.rollInputEmitter.next(input);
  }
  private simulateCd(hero: PocketHero, propertyType: RollOrigin, propertyId: string) {
    const input = {
      propertyType: propertyType,
      creatureId: hero.id,
    } as SimulateCdInput;
    if (propertyType === RollOrigin.Attribute) {
      const attribute = hero.attributes.find(a => a.attributeTemplateId === propertyId);
      input.propertyId = attribute.id;
    } else if (propertyType === RollOrigin.Skill) {
      const skill = hero.skills.find(s => s.skillTemplateId === propertyId);
      input.propertyId = skill.id;
    } else if (propertyType === RollOrigin.MinorSkill) {
      const skill = hero.skills.find(s => s.minorSkills.some(m => m.minorSkillTemplateId === propertyId));
      const minorSkills = skill.minorSkills.find(m => m.minorSkillTemplateId === propertyId);
      input.propertyId = minorSkills.id;
    }
    this.displaySimulateCdSidebar = true;
    this.simulateCdInputEmitter.next(input);
  }
  public takeDamage(hero: PocketHero) {
    const input = {
      creature: hero
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
