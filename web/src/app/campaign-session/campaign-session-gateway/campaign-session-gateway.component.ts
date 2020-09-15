import { Component, OnInit } from '@angular/core';
import {CampaignSessionService} from '../campaign-session.service';
import {ActivatedRoute, Router} from '@angular/router';
import {Campaign} from '../../shared/models/campaign/Campaign.model';

@Component({
  selector: 'loh-campaign-session-gateway',
  templateUrl: './campaign-session-gateway.component.html',
  styleUrls: ['./campaign-session-gateway.component.css']
})
export class CampaignSessionGatewayComponent implements OnInit {

  private campaignId: string;
  public campaign: Campaign;
  constructor(
    private readonly route: ActivatedRoute,
    private readonly campaignSessionService: CampaignSessionService
  ) {
    this.campaignId = this.route.snapshot.params['campaignId'];
    this.campaignSessionService.getCampaign(this.campaignId).subscribe((campaign: Campaign) => {
      this.campaign = campaign;
    });
  }

  ngOnInit(): void {
  }

}
