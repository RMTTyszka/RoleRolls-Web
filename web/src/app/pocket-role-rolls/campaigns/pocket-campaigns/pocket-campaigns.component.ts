import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseComponentConfig } from 'src/app/shared/components/base-component-config';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { PocketCampaignsService } from '../pocket-campaigns.service';
import { PocketCampaignConfig } from '../pocket.campaign.config';

@Component({
  selector: 'rr-pocket-campaigns',
  templateUrl: './pocket-campaigns.component.html',
  styleUrls: ['./pocket-campaigns.component.scss']
})
export class PocketCampaignsComponent implements OnInit {

  public config = new PocketCampaignConfig();
  constructor(
    public service: PocketCampaignsService,
    public router: Router,
    ) {
      this.config.entityListActions.push({
        icon: 'pi pi-eject',
        callBack: ((entity: PocketCampaignModel) => {
          this.router.navigate([`pocket/campaigns/${entity.id}`]);
        })
     });
    }

  ngOnInit(): void {
  }

}
