import {Component, OnDestroy, OnInit} from '@angular/core';
import {PocketCampaignDetailsService} from "../pocket-campaign-bodyshell/pocket-campaign-details.service";
import {SubscriptionManager} from "../../../shared/utils/subscription-manager";
import {PocketCampaignModel} from "../../../shared/models/pocket/campaigns/pocket.campaign.model";
import {CampaignScene} from "../../../shared/models/pocket/campaigns/campaign-scene-model";
import {SceneNotificationService} from "../scene-notification.service";

@Component({
  selector: 'rr-campaign-history',
  templateUrl: './campaign-history.component.html',
  styleUrls: ['./campaign-history.component.scss']
})
export class CampaignHistoryComponent implements OnInit, OnDestroy {

  public history: string[] = [];
  private subscriptionManager = new SubscriptionManager();
  private scene: CampaignScene;
  constructor(
    private campaignDetailsService: PocketCampaignDetailsService,
    private sceneNotificationService: SceneNotificationService,
    ) {
    this.subscriptionManager.add('sceneChanged', this.campaignDetailsService.sceneChanged.subscribe((scene: CampaignScene) => {
      this.scene = scene;
      if (scene) {
        this.sceneNotificationService.joinScene(scene.id)
      }
    }))
    this.subscriptionManager.add('historyUpdated', this.sceneNotificationService.historyUpdated.subscribe((history: string) => {
      console.log(history);
    }))
  }

  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }

  ngOnInit(): void {
  }

}
