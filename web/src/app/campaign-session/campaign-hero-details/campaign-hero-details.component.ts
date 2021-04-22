import {Component, Input, OnInit} from '@angular/core';
import {Hero} from '../../shared/models/NewHero.model';
import {Campaign} from '../../shared/models/campaign/Campaign.model';
import {CampaignSessionService} from '../campaign-session.service';

@Component({
  selector: 'rr-campaign-hero-details',
  templateUrl: './campaign-hero-details.component.html',
  styleUrls: ['./campaign-hero-details.component.scss']
})
export class CampaignHeroDetailsComponent implements OnInit {

  @Input() selectedHero: Hero;
  @Input() isMaster = false;
  campaign: Campaign;
  constructor(
    private campaignSessionService: CampaignSessionService
  ) {
    this.campaignSessionService.campaignChanged.subscribe(campaign => this.campaign = campaign);
  }

  ngOnInit(): void {
  }

}
