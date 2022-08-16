import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { EditorAction } from 'src/app/shared/dtos/ModalEntityData';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { PocketCampaignsService } from '../pocket-campaigns.service';

@Component({
  selector: 'rr-campaign-creator',
  templateUrl: './campaign-creator.component.html',
  styleUrls: ['./campaign-creator.component.scss']
})
export class CampaignCreatorComponent implements OnInit {
  public action = EditorAction.create;
  public form = new FormGroup({});
  public isLoading = true;
  public entity: PocketCampaignModel;
  public entityId: string;
  public requiredFields = ['name'];

  constructor(
    public service: PocketCampaignsService,
    public ref: DynamicDialogRef,
  ) { }

  ngOnInit(): void {
  }


  loaded(entity: PocketCampaignModel) {
    this.isLoading = false;
    this.entity = entity;
  }
  deleted() {
    this.ref.close(true);
  }

  saved() {
    this.ref.close(true);
  }

}
