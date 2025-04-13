import {Component, input, signal} from '@angular/core';
import {CampaignScene} from '@app/campaigns/models/campaign-scene-model';
import {SubscriptionManager} from '@app/tokens/subscription-manager';
import {AuthenticationService} from '@app/authentication/services/authentication.service';
import {Creature} from '@app/campaigns/models/creature';
import {CreatureCategory} from '@app/campaigns/models/CreatureCategory';
import {CampaignsService} from '@app/campaigns/services/campaigns.service';
import {CampaignSessionService} from '@app/campaign-session/campaign-session.service';
import {Panel} from 'primeng/panel';
import {SceneCreatureRowComponent} from '@app/campaign-session/scene-creature-row/scene-creature-row.component';
import {NgForOf, NgIf} from '@angular/common';
import {SceneCreature} from '@app/campaigns/models/scene-creature.model';
import {CreatureSelectComponent} from '@app/campaign-session/creature-select/creature-select.component';
import {firstValueFrom} from 'rxjs';
import {ActivatedRoute} from '@angular/router';
import {Campaign} from '@app/campaigns/models/campaign';
import {ButtonDirective} from 'primeng/button';
import {EncountersService} from '@app/encounters/services/encounters.service';
import {Encounter} from '@app/encounters/models/encounter';

  @Component({
    selector: 'rr-scene-creatures',
    imports: [
      Panel,
      SceneCreatureRowComponent,
      NgForOf,
      CreatureSelectComponent,
      NgIf,
      ButtonDirective
    ],
    templateUrl: './scene-creatures.component.html',
    styleUrl: './scene-creatures.component.scss'
  })
export class SceneCreaturesComponent {

  public creatureCategory = input<CreatureCategory>();
  public scene: CampaignScene;
  public creatures: Creature[] = [];
  protected readonly CreatureCategory = CreatureCategory;
  private subscriptionManager = new SubscriptionManager();
  public campaign = signal<Campaign>(null);
  public isMaster = signal<boolean>(false);

  constructor(
    private readonly campaignService: CampaignsService,
    private readonly detailsService: CampaignSessionService,
    private authenticationService: AuthenticationService,
    private readonly encountersService: EncountersService,
    private readonly route: ActivatedRoute,
  ) {
    this.subscribeToSceneChanges();

    this.subscribeToCreatureTookDamage();
  }
  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.campaign.set(data['campaign']);
      this.isMaster.set(this.authenticationService.userId === this.campaign().masterId);
    });
    this.subscribeToEnemyRemoved();
    this.subscribeToAllyRemoved();
  }

  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }

  private refreshCreatures(keepObjects: boolean) {
    this.campaignService.getSceneCreatures(this.scene.campaignId, this.scene.id, this.creatureCategory())
      .subscribe((creatures: Creature[]) => {
        if (!keepObjects) {
          this.creatures = creatures as Creature[];
        } else {
          this.creatures.forEach(creature => {
            const updatedCreature = creatures.filter(h => h.id == creature.id)[0];
            if (updatedCreature) {
              Object.assign(creature, updatedCreature);
            }
          });
        }
        this.detailsService.heroes.set(this.creatures);
      });
  }
  private subscribeToSceneChanges() {
    this.subscriptionManager.add('sceneChanged', this.detailsService.sceneChanged.subscribe((scene: CampaignScene) => {
      this.scene = scene;
      if (this.scene) {
        this.refreshCreatures(false);
      }
    }));
  }
  private subscribeToAllyRemoved() {
    if (this.creatureCategory() === CreatureCategory.Ally) {
      this.subscriptionManager.add('subscribeToCreatureRemoved', this.detailsService.heroRemovedToScene.subscribe(() => {
        this.refreshCreatures(false);
      }));
    }

  }
  private subscribeToEnemyRemoved() {
    if (this.creatureCategory() === CreatureCategory.Enemy) {
      this.subscriptionManager.add('monsterRemovedToScene', this.detailsService.monsterRemovedToScene.subscribe(() => {
        this.refreshCreatures(false);
      }));
    }
  }

  private subscribeToCreatureTookDamage() {
    this.subscriptionManager.add('creatureTookDamage', this.detailsService.heroTookDamage.subscribe(() => {
      this.refreshCreatures(true);
    }));
  }
  public addCreature(creature: Creature) {
    const input = {
      creatureId: creature.id,
      hidden: false
    } as SceneCreature;
    if (creature.category === CreatureCategory.Ally) {
      this.campaignService.addHeroToScene(this.scene.campaignId, this.scene.id, input)
        .subscribe(() => {
          this.refreshCreatures(false);

        });
    } else {
      this.campaignService.addMonsterToScene(this.scene.campaignId, this.scene.id, input)
        .subscribe(() => {
          this.refreshCreatures(false);

        });
    }
  }
  public async addCreatures(creatures: Creature[]): Promise<void> {
    for (const creature of creatures) {
      const input = {
        creatureId: creature.id,
        hidden: false
      } as SceneCreature;

      if (creature.category === CreatureCategory.Ally) {
        await firstValueFrom(this.campaignService.addHeroToScene(this.scene.campaignId, this.scene.id, input));
      } else {
        await firstValueFrom(this.campaignService.addMonsterToScene(this.scene.campaignId, this.scene.id, input));
      }
    }
    this.refreshCreatures(false);
  }

  public selectEncounter() {
    this.encountersService.openSelectEncounter(this.campaign())
      .subscribe(async (encounter: Encounter) => {
        if (encounter) {
          encounter = await firstValueFrom(this.encountersService.getById(this.campaign().id, encounter.id))
          await this.addCreatures(encounter.creatures);
        }
      })
  }
}
