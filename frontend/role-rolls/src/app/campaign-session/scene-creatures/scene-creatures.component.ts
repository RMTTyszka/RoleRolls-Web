import {Component, input} from '@angular/core';
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
import {NgForOf} from '@angular/common';

@Component({
  selector: 'rr-scene-creatures',
  imports: [
    Panel,
    SceneCreatureRowComponent,
    NgForOf
  ],
  templateUrl: './scene-creatures.component.html',
  styleUrl: './scene-creatures.component.scss'
})
export class SceneCreaturesComponent {

  public creatureCategory = input<CreatureCategory>();
  public scene: CampaignScene;
  public heroes: Creature[] = [];
  private subscriptionManager = new SubscriptionManager();

  constructor(
    private readonly campaignService: CampaignsService,
    private readonly detailsService: CampaignSessionService,
    private authService: AuthenticationService,
    private readonly dialogService: DialogService,
  ) {
    this.subscribeToSceneChanges();
    this.subscribeToCreatureAdded();
    this.subscribeToCreatureRemoved();
    this.subscribeToCreatureTookDamage();
  }
  ngOnInit(): void {
  }
  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }

  private refreshCreaturees(keepObjects: boolean) {
    this.campaignService.getSceneCreatures(this.scene.campaignId, this.scene.id, CreatureCategory.Hero)
      .subscribe((heroes: Creature[]) => {
        if (!keepObjects) {
          this.heroes = heroes as Creature[];
        } else {
          this.heroes.forEach(creature => {
            const updatedCreature = heroes.filter(h => h.id == creature.id)[0];
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
        this.refreshCreaturees(false);
      }
    }));
  }

  private subscribeToCreatureAdded() {
    this.subscriptionManager.add('creatureAddedToScene', this.detailsService.heroAddedToScene.subscribe(() => {
      this.refreshCreaturees(false);
    }));
  }
  private subscribeToCreatureRemoved() {
    this.subscriptionManager.add('subscribeToCreatureRemoved', this.detailsService.heroRemovedToScene.subscribe(() => {
      this.refreshCreaturees(false);
    }));
  }

  private subscribeToCreatureTookDamage() {
    this.subscriptionManager.add('creatureTookDamage', this.detailsService.heroTookDamage.subscribe(() => {
      this.refreshCreaturees(true);
    }));
  }

  protected readonly CreatureCategory = CreatureCategory;
}
