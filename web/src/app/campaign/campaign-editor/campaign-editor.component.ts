import { Component, OnInit } from '@angular/core';
import {EditorAction} from '../../shared/dtos/ModalEntityData';
import {FormGroup} from '@angular/forms';
import {DialogService, DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {Campaign} from '../../shared/models/campaign/Campaign.model';
import {CampaignsService} from '../campaigns.service';
import {PlayerSelectModalComponent} from '../../players/player-shared/player-select-modal/player-select-modal.component';
import {Player} from '../../shared/models/Player.model';
import {CampaignPlayerSelectComponent} from '../campaign-player-select/campaign-player-select.component';

@Component({
  selector: 'loh-campaign-editor',
  templateUrl: './campaign-editor.component.html',
  styleUrls: ['./campaign-editor.component.css'],
  providers: [DialogService]
})
export class CampaignEditorComponent implements OnInit {
  entity: Campaign;
  action: EditorAction;
  form: FormGroup = new FormGroup({});
  isLoading = true;
  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    public dialogService: DialogService,
    public service: CampaignsService,
  ) {
    this.action = config.data.action;
    if (config.data.action === EditorAction.create) {
      this.entity = new Campaign();
    } else {
      this.entity = config.data.entity;
    }
  }

  ngOnInit() {
  }
  loaded(hasLoaded) {
    this.isLoading = !hasLoaded;
  }

  saved(campaign: Campaign) {
    this.ref.close(campaign);
  }
  deleted(campaign: Campaign) {
    this.ref.close(campaign);
  }

  addPlayer() {
    this.dialogService.open(CampaignPlayerSelectComponent, {
      height: '100%',
      width: '50%',
      data: {
        campaignId: this.entity.id
      }
    }).onClose.subscribe((player: Player) => {
      this.service.invitePlayer(this.entity.id, player.id).subscribe();
    });
  }
}
