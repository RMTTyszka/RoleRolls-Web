import { Component, OnInit } from '@angular/core';
import {CampaignsService} from '../campaigns.service';
import {InvitedPlayer} from '../../shared/models/campaign/InvitedPlayer.model';
import {Campaign} from '../../shared/models/campaign/Campaign.model';

@Component({
  selector: 'loh-campaign-invitation',
  templateUrl: './campaign-invitation.component.html',
  styleUrls: ['./campaign-invitation.component.css']
})
export class CampaignInvitationComponent implements OnInit {
  campaigns: Campaign[];
  constructor(
    private service: CampaignsService
  ) { }

  ngOnInit(): void {
    this.service.getInvitations().subscribe(invitations => {
      this.campaigns = invitations.campaigns;
    });
  }

  accept(campaign: Campaign) {
    this.service.acceptInvitation(campaign.id).subscribe(() => {
      this.campaigns = this.campaigns.filter(c => c.id !== campaign.id);
    });
  }
  deny(campaign: Campaign) {
    this.service.denyInvitation(campaign.id).subscribe(() => {
      this.campaigns = this.campaigns.filter(c => c.id !== campaign.id);
    });
  }

}
