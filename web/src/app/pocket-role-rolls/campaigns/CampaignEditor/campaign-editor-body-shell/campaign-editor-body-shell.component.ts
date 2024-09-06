import {Component, OnInit, signal} from '@angular/core';
import {DynamicDialogConfig} from "primeng/dynamicdialog";
import {EditorAction} from "src/app/shared/dtos/ModalEntityData";
import {PocketCampaignModel} from "src/app/shared/models/pocket/campaigns/pocket.campaign.model";

@Component({
  selector: 'rr-campaign-editor-body-shell',
  templateUrl: './campaign-editor-body-shell.component.html',
  styleUrls: ['./campaign-editor-body-shell.component.scss']
})
export class CampaignEditorBodyShellComponent implements OnInit {
  public action = EditorAction.create;
  public entityId: string;
  public campaign: PocketCampaignModel;
  constructor(
    public config: DynamicDialogConfig,

  ) {
    this.action = config.data.action;
    this.entityId = config.data.entityId;
  }

  ngOnInit(): void {

  }

  protected readonly caches = caches;

  campaignLoaded($event: PocketCampaignModel) {
    this.campaign = $event;
  }
}

