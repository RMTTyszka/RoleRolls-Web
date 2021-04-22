import { Component, OnInit } from '@angular/core';
import {CampaignsService} from '../campaigns.service';
import {InvitedPlayer} from '../../shared/models/campaign/InvitedPlayer.model';
import {Campaign} from '../../shared/models/campaign/Campaign.model';
import {DialogService} from 'primeng/dynamicdialog';
import {HeroSelectComponent} from '../../heroes/heroes-shared/hero-select/hero-select.component';
import {Hero} from '../../shared/models/NewHero.model';
import {HeroesSelectModalComponent} from '../../heroes/heroes-shared/heroes-select-modal/heroes-select-modal.component';

@Component({
  selector: 'rr-campaign-invitation',
  templateUrl: './campaign-invitation.component.html',
  styleUrls: ['./campaign-invitation.component.css']
})
export class CampaignInvitationComponent implements OnInit {
  campaigns: Campaign[];
  constructor(
    private service: CampaignsService,
    private dialogService: DialogService
  ) { }

  ngOnInit(): void {
    this.service.getInvitations().subscribe(invitations => {
      this.campaigns = invitations.campaigns;
    });
  }

  accept(campaign: Campaign) {
    this.dialogService.open(HeroesSelectModalComponent, {
      header: 'Your Heroes',
      data: {
        campaignId: campaign.id
      }
    }).onClose.subscribe(( hero: Hero) => {
      this.service.acceptInvitation(campaign.id).subscribe(() => {
        this.campaigns = this.campaigns.filter(c => c.id !== campaign.id);
        this.service.addHero(campaign.id, hero.id).subscribe();
      });

    });

  }
  deny(campaign: Campaign) {
    this.service.denyInvitation(campaign.id).subscribe(() => {
      this.campaigns = this.campaigns.filter(c => c.id !== campaign.id);
    });
  }

}
