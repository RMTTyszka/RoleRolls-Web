import { Component, input, signal } from '@angular/core';
import { CampaignScene } from '@app/campaigns/models/campaign-scene-model';
import { SubscriptionManager } from '@app/tokens/subscription-manager';
import { AuthenticationService } from '@app/authentication/services/authentication.service';
import { DialogService } from 'primeng/dynamicdialog';
import { Creature } from '@app/campaigns/models/creature';
import { CreatureCategory } from '@app/campaigns/models/CreatureCategory';
import { CampaignsService } from '@app/campaigns/services/campaigns.service';
import { CampaignSessionService } from '@app/campaign-session/campaign-session.service';
import { Panel } from 'primeng/panel';
import { SceneCreatureRowComponent } from '@app/campaign-session/scene-creature-row/scene-creature-row.component';
import { NgForOf, NgIf } from '@angular/common';
import { SceneCreature } from '@app/campaigns/models/scene-creature.model';
import { CreatureSelectComponent } from '@app/campaign-session/creature-select/creature-select.component';
import { switchMap } from 'rxjs/operators';
import { forkJoin } from 'rxjs';
import { MenuItem } from 'primeng/api';
import { ActivatedRoute } from '@angular/router';
import { Campaign } from '@app/campaigns/models/campaign';

@Component({
  selector: 'rr-scene-creatures',
  imports: [
    Panel,
    SceneCreatureRowComponent,
    NgForOf,
    CreatureSelectComponent,
    NgIf
  ],
  templateUrl: './scene-creatures.component.html',
  styleUrl: './scene-creatures.component.scss'
})
export class SceneCreaturesComponent {

  public creatureCategory = input<CreatureCategory>();
  public scene: CampaignScene;
  public heroes: Creature[] = [];
  protected readonly CreatureCategory = CreatureCategory;
  private subscriptionManager = new SubscriptionManager();
  public campaign = signal<Campaign>(null);
  public isMaster = signal<boolean>(false);

  constructor(
    private readonly campaignService: CampaignsService,
    private readonly detailsService: CampaignSessionService,
    private authenticationService: AuthenticationService,
    private readonly dialogService: DialogService,
    private readonly route: ActivatedRoute,
  ) {
    this.subscribeToSceneChanges();
    this.subscribeToCreatureAdded();
    this.subscribeToCreatureRemoved();
    this.subscribeToCreatureTookDamage();
  }
  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.campaign.set(data['campaign']);
      this.isMaster.set(this.authenticationService.userId === this.campaign().masterId);
    });
  }

  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }

  private refreshCreatures(keepObjects: boolean) {
    this.campaignService.getSceneCreatures(this.scene.campaignId, this.scene.id, this.creatureCategory())
      .subscribe((creatures: Creature[]) => {
        if (!keepObjects) {
          this.heroes = creatures as Creature[];
        } else {
          this.heroes.forEach(creature => {
            const updatedCreature = creatures.filter(h => h.id == creature.id)[0];
            if (updatedCreature) {
              Object.assign(creature, updatedCreature);
            }
          });
        }
        this.detailsService.heroes.set(this.heroes);
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

  private subscribeToCreatureAdded() {
    this.subscriptionManager.add('creatureAddedToScene', this.detailsService.heroAddedToScene.subscribe(() => {
      this.refreshCreatures(false);
    }));
  }
  private subscribeToCreatureRemoved() {
    this.subscriptionManager.add('subscribeToCreatureRemoved', this.detailsService.heroRemovedToScene.subscribe(() => {
      this.refreshCreatures(false);
    }));
  }

  private subscribeToCreatureTookDamage() {
    this.subscriptionManager.add('creatureTookDamage', this.detailsService.heroTookDamage.subscribe(() => {
      this.refreshCreatures(true);
    }));
  }
  public addCreature(creature: Creature){
    const input = {
      creatureId: creature.id,
      hidden: false
    } as SceneCreature;
    if (creature.category === CreatureCategory.Ally) {
      this.campaignService.addHeroToScene(this.scene.campaignId, this.scene.id, input)
        .subscribe(() => {
          this.detailsService.heroAddedToScene.next();
        });
    } else {
      this.campaignService.addMonsterToScene(this.scene.campaignId, this.scene.id, input)
        .subscribe(() => {
          this.detailsService.monsterAddedToScene.next();
        });
    }

  }
}
