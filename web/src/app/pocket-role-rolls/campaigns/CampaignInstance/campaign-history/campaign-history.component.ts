import {Component, OnDestroy, OnInit} from '@angular/core';
import {PocketCampaignDetailsService} from "../pocket-campaign-bodyshell/pocket-campaign-details.service";
import {HistoryDto} from "../../models/history-dto";
import {SubscriptionManager} from "../../../../shared/utils/subscription-manager";
import {CampaignScene} from "../../../../shared/models/pocket/campaigns/campaign-scene-model";
import {PocketCampaignModel} from "../../../../shared/models/pocket/campaigns/pocket.campaign.model";
import {SceneNotificationService} from "../Services/scene-notification.service";
import {CampaignSceneHistoryService} from "../Services/campaign-scene-history.service";

@Component({
  selector: 'rr-campaign-history',
  templateUrl: './campaign-history.component.html',
  styleUrls: ['./campaign-history.component.scss']
})
export class CampaignHistoryComponent implements OnInit, OnDestroy {

  public histories: HistoryDto[] = [];
  private subscriptionManager = new SubscriptionManager();
  private scene: CampaignScene;
  private campaign: PocketCampaignModel;
  constructor(
    private campaignDetailsService: PocketCampaignDetailsService,
    private sceneNotificationService: SceneNotificationService,
    private campaignSceneHistoryService: CampaignSceneHistoryService,
    ) {
    this.subscriptionManager.add('historyUpdated', this.sceneNotificationService.historyUpdated.subscribe((history: HistoryDto) => {
      console.log(history);
      if (!this.histories.find(h => h.asOfDate == history.asOfDate)) {
        this.histories.unshift(history);
      }
    }))
    this.subscriptionManager.add('campaignLoaded', this.campaignDetailsService.campaignLoaded.subscribe((campaignModel: PocketCampaignModel) => {
      if (campaignModel) {
        this.campaign = campaignModel;
        this.subscriptionManager.add('sceneChanged', this.campaignDetailsService.sceneChanged.subscribe((campaignScene: CampaignScene) => {
          if (campaignScene) {
            this.scene = campaignScene;
            this.sceneNotificationService.joinScene(campaignScene.id)
            this.campaignSceneHistoryService.getHistory(this.campaign.id, this.scene .id).subscribe((histories: HistoryDto[]) => {
              this.histories = histories;
            });
          }
        }))
      }

    }))
  }

  ngOnDestroy(): void {
    this.subscriptionManager.clear();
  }

  ngOnInit(): void {
  }

}
