import {Component, OnDestroy, OnInit} from '@angular/core';
import {PocketCampaignDetailsService} from "../pocket-campaign-bodyshell/pocket-campaign-details.service";
import {SubscriptionManager} from "../../../shared/utils/subscription-manager";
import {PocketCampaignModel} from "../../../shared/models/pocket/campaigns/pocket.campaign.model";
import {CampaignScene} from "../../../shared/models/pocket/campaigns/campaign-scene-model";
import {SceneNotificationService} from "../scene-notification.service";
import {HistoryDto} from "../models/history-dto";
import {CampaignSceneHistoryService} from "../campaign-scene-history.service";

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
    this.subscriptionManager.add('sceneChanged', this.campaignDetailsService.sceneChanged.subscribe((scene: CampaignScene) => {
      this.scene = scene;
      if (scene) {
        this.sceneNotificationService.joinScene(scene.id)
      }
    }))
    this.subscriptionManager.add('historyUpdated', this.sceneNotificationService.historyUpdated.subscribe((history: HistoryDto) => {
      console.log(history);
      if (!this.histories.find(h => h.dateTime == history.dateTime)) {
        this.histories.push(history);
      }
    }))
    this.subscriptionManager.add('campaignLoaded', this.campaignDetailsService.campaignLoaded.subscribe((campaignModel: PocketCampaignModel) => {
      if (campaignModel) {
        this.campaign = campaignModel;
        this.subscriptionManager.add('sceneChanged', this.campaignDetailsService.sceneChanged.subscribe((campaignScene: CampaignScene) => {
          if (campaignScene) {
            this.scene = campaignScene;
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
