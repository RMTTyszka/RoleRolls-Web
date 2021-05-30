import { Component, OnInit } from '@angular/core';
import {CampaignSessionService} from '../campaign-session.service';
import {ActivatedRoute, Router} from '@angular/router';
import {Campaign} from '../../shared/models/campaign/Campaign.model';
import {CampaignCombatHeroService} from '../../creatures-shared/creature-base-select/campaign-combat-hero.service';

@Component({
  selector: 'rr-campaign-session-gateway',
  templateUrl: './campaign-session-gateway.component.html',
  styleUrls: ['./campaign-session-gateway.component.css'],
  providers: [CampaignSessionService, CampaignCombatHeroService]
})
export class CampaignSessionGatewayComponent implements OnInit {

  public campaignId: string;
  public campaign: Campaign = new Campaign();
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
