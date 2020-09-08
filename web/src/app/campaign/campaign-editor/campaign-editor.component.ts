import {Component, OnInit} from '@angular/core';
import {EditorAction} from '../../shared/dtos/ModalEntityData';
import {FormGroup} from '@angular/forms';
import {DialogService, DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {Campaign} from '../../shared/models/campaign/Campaign.model';
import {CampaignsService} from '../campaigns.service';
import {Player} from '../../shared/models/Player.model';
import {CampaignPlayerSelectComponent} from '../campaign-player-select/campaign-player-select.component';
import {CampaignInvitationsOutput} from '../../shared/models/campaign/dtos/CampaignInvitationsOutput';
import {InvitationStatus} from '../../shared/models/campaign/InvitationStatus';

@Component({
  selector: 'loh-campaign-editor',
  templateUrl: './campaign-editor.component.html',
  styleUrls: ['./campaign-editor.component.css'],
  providers: [DialogService]
})
export class CampaignEditorComponent implements OnInit {
  entity: Campaign;
  invitations: CampaignInvitationsOutput[];
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
      this.service.getCampaignInvitations(this.entity.id).subscribe((invitations: CampaignInvitationsOutput[]) => {
        this.invitations = invitations;
      });
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
      this.invitations.push({player: player, status: InvitationStatus.Sent});
    });
  }

  removePlayer(player: Player) {
    this.service.removePlayer(this.entity.id, player.id).subscribe(() => {
      this.entity.players.splice(this.entity.players.indexOf(player), 1);
    });
  }

  removeInvitation(player: Player) {
    this.service.removeInvitation(this.entity.id, player.id).subscribe(() => {
      this.invitations.splice(this.invitations.findIndex(p => p.player.id === player.id), 1);
    });
  }
}
