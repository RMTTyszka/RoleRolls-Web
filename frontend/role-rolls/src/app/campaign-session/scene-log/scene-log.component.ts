import { Component } from '@angular/core';
import { Card } from 'primeng/card';
import { LogDetailsComponent } from '@app/campaign-session/scene-log/log-details/log-details.component';
import { DatePipe } from '@angular/common';
import { HistoryDto } from '@app/campaigns/models/history-dto';
import { SubscriptionManager } from '@app/tokens/subscription-manager';
import { CampaignScene } from '@app/campaigns/models/campaign-scene-model';
import { Campaign } from '@app/campaigns/models/campaign';
import { CampaignSessionService } from '@app/campaign-session/campaign-session.service';
import { SceneNotificationService } from '@app/campaign-session/services/scene-notification.service';
import { CampaignSceneLogService } from '@services/scene-logs/campaign-scene-log.service';

@Component({
  selector: 'rr-scene-log',
  imports: [
    Card,
    LogDetailsComponent,
    DatePipe
  ],
  templateUrl: './scene-log.component.html',
  styleUrl: './scene-log.component.scss'
})
export class SceneLogComponent {

  public histories: HistoryDto[] = [];
  private subscriptionManager = new SubscriptionManager();
  private scene: CampaignScene;
  private campaign: Campaign;
  constructor(
    private campaignDetailsService: CampaignSessionService,
    private sceneNotificationService: SceneNotificationService,
    private campaignSceneHistoryService: CampaignSceneLogService,
  ) {
    this.subscriptionManager.add('historyUpdated', this.sceneNotificationService.historyUpdated.subscribe((history: HistoryDto) => {
      console.log(history);
      if (!this.histories.find(h => h.asOfDate == history.asOfDate)) {
        this.histories.unshift(history);
      }
    }))
    this.subscriptionManager.add('campaignLoaded', this.campaignDetailsService.campaignLoaded.subscribe((campaignModel: Campaign) => {
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
