import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { forkJoin } from 'rxjs';
import { CampaignScene } from 'src/app/shared/models/pocket/campaigns/campaign-scene-model';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { PocketCampaignsService } from '../pocket-campaigns.service';

@Component({
  selector: 'rr-pocket-campaign-bodyshell',
  templateUrl: './pocket-campaign-bodyshell.component.html',
  styleUrls: ['./pocket-campaign-bodyshell.component.scss']
})
export class PocketCampaignBodyshellComponent implements OnInit {

  public campaignId: string;
  public campaign: PocketCampaignModel = new PocketCampaignModel();

  public selectedScene: CampaignScene;
  public newSceneName: string;
  public scenes: CampaignScene[] = [];
  public menuItens: MenuItem[] = [];
  constructor(
    private readonly route: ActivatedRoute,
    private readonly campaignService: PocketCampaignsService,
  ) {
    this.campaignId = this.route.snapshot.params['id'];
    forkJoin({
      campaign: this.campaignService.get(this.campaignId),
      scenes: this.campaignService.getScenes(this.campaignId)
    })
    .subscribe((result) => {
      this.campaign = result.campaign;
      this.scenes = result.scenes.content;
    });
    this.menuItens = [
      {
        icon: 'fist-raised'
      } as MenuItem,
    ];
   }

  ngOnInit(): void {
  }
  public newScene() {
  }

}
