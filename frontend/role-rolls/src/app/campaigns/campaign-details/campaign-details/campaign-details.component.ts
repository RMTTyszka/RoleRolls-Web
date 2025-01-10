import { Component } from '@angular/core';
import { EditorAction } from '../../../models/ModalEntityData';
import { Campaign } from '../../models/pocket.campaign.model';
import { Campaign } from '../../models/campaign';

@Component({
  selector: 'rr-campaign-details',
  standalone: false,

  templateUrl: './campaign-details.component.html',
  styleUrl: './campaign-details.component.scss'
})
export class CampaignDetailsComponent {
  public action = EditorAction.create;
  public actionEnum = EditorAction;
  public entityId!: string;
  public campaign!: Campaign;
  constructor(
  ) {
/*    this.action = config.data.action;
    this.entityId = config.data.entityId;*/
  }

  ngOnInit(): void {

  }

  protected readonly caches = caches;

  campaignLoaded($event: Campaign) {
    this.campaign = $event;
  }
}

