import { Component, OnInit } from '@angular/core';
import {PocketCampaignDetailsService} from "../pocket-campaign-bodyshell/pocket-campaign-details.service";

@Component({
  selector: 'rr-campaign-history',
  templateUrl: './campaign-history.component.html',
  styleUrls: ['./campaign-history.component.scss']
})
export class CampaignHistoryComponent implements OnInit {

  public history: string[] = [];
  constructor(private campaignDetailsService: PocketCampaignDetailsService) { }

  ngOnInit(): void {
  }

}
