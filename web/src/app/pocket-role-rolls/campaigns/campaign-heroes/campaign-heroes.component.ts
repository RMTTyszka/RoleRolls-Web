import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/authentication/authentication.service';
import { CreatureType } from 'src/app/shared/models/creatures/CreatureType';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { PocketHero } from 'src/app/shared/models/pocket/creatures/pocket-creature';
import { SubscriptionManager } from 'src/app/shared/utils/subscription-manager';
import { PocketCampaignDetailsService } from '../pocket-campaign-bodyshell/pocket-campaign-details.service';
import { PocketCampaignsService } from '../pocket-campaigns.service';

@Component({
  selector: 'rr-campaign-heroes',
  templateUrl: './campaign-heroes.component.html',
  styleUrls: ['./campaign-heroes.component.scss']
})
export class CampaignHeroesComponent implements OnInit, OnDestroy {
  public heroes: PocketHero[] = [];

  public get isMaster() {
    return this.campaign.masterId === this.authService.userId;
  }
  private subscriptionManager = new SubscriptionManager();
  private scene: CampaignScene = new CampaignScene();
  private campaign: PocketCampaignModel = new PocketCampaignModel();
  constructor(
    private readonly campaignService: PocketCampaignsService,
    private readonly detailsService: PocketCampaignDetailsService,
    private authService: AuthenticationService,
  ) {
    this.subscribeToCampaignLoaded();
    this.subscribeToSceneChanges();
   }

  private subscribeToCampaignLoaded() {
    this.subscriptionManager.add('campaignLoaded', this.detailsService.campaignLoaded.subscribe((campaign: PocketCampaignModel) => {
        this.campaign = campaign;
    }));
  }

  private subscribeToSceneChanges() {
    this.subscriptionManager.add('sceneChanged', this.detailsService.sceneChanged.subscribe((scene: CampaignScene) => {
        this.scene = scene;
        this.refreshHeroes();
    }));
  }
  private refreshHeroes() {
    this.campaignService.getSceneCreatures(this.scene.campaignId, this.scene.id, CreatureType.Hero);
  }

  ngOnInit(): void {
  }
  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }
}
