import { Component, OnInit } from '@angular/core';
import { BaseComponentConfig } from 'src/app/shared/components/base-component-config';
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
    public service: PocketCampaignsService
    ) { }

  ngOnInit(): void {
  }

}
