import { SimulateCdInput } from './../models/SimulateCdInput';
import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
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

  public scene: CampaignScene = new CampaignScene();
  public heroes: PocketCreature[] = [];
  private subscriptionManager = new SubscriptionManager();

  constructor(
    private readonly campaignService: PocketCampaignsService,
    private readonly detailsService: PocketCampaignDetailsService,
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
    this.campaignService.getSceneCreatures(this.scene.campaignId, this.scene.id, CreatureType.Hero)
    .subscribe((heroes: PocketCreature[]) => {
      if (!keepObjects) {
        this.heroes = heroes as PocketCreature[];
      } else {
        this.heroes.forEach(creature => {
          const updatedCreature = heroes.filter(h => h.id == creature.id)[0];
          if (updatedCreature) {
            Object.assign(creature, updatedCreature)
          }
        });
      }
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
}
