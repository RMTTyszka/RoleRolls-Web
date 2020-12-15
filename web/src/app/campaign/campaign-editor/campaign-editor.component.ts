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
import {Hero} from '../../shared/models/NewHero.model';
import {AuthenticationService} from '../../authentication/authentication.service';
import {HeroesSelectModalComponent} from '../../heroes/heroes-shared/heroes-select-modal/heroes-select-modal.component';
import {MessageService} from 'primeng/api';
import {Router} from '@angular/router';
import {of} from 'rxjs';

@Component({
  selector: 'loh-campaign-editor',
  templateUrl: './campaign-editor.component.html',
  styleUrls: ['./campaign-editor.component.css'],
  providers: [DialogService]
})
export class CampaignEditorComponent implements OnInit {
  entity: Campaign;
  entityId: string;
  invitations: CampaignInvitationsOutput[];
  action: EditorAction;
  form: FormGroup = new FormGroup({});
  isLoading = true;
  constructor(
    public ref: DynamicDialogRef,
    private router: Router,
    public config: DynamicDialogConfig,
    public dialogService: DialogService,
    public messageService: MessageService,
    public authenticationService: AuthenticationService,
    public service: CampaignsService
  ) {
    this.action = config.data.action;
    if (config.data.action === EditorAction.create) {
    } else {
      this.entityId = config.data.entityId;
    }
  }

  async ngOnInit() {
    this.invitations = await this.getInvitations();
  }

  loaded(entity) {
    this.isLoading = false;
    this.entity = entity;
  }

  saved(campaign: Campaign) {
    this.ref.close(campaign);
  }
  deleted(campaign: Campaign) {
    this.ref.close(campaign);
  }

  invitePlayer() {
    this.dialogService.open(CampaignPlayerSelectComponent, {
      height: '100%',
      width: '50%',
      data: {
        campaignId: this.entity.id
      }
    }).onClose.subscribe((player: Player) => {
      this.service.invitePlayer(this.entity.id, player.id).subscribe();
      this.invitations.push({player: player, status: InvitationStatus.Sent});
      this.messageService.add({
        summary: `${player.name} successfully invited to campaign`,
        severity: 'success'
      });
    });
  }

  removePlayer(player: Player) {
    this.service.removePlayer(this.entity.id, player.id).subscribe(() => {
      this.entity.players.splice(this.entity.players.indexOf(player), 1);
      this.messageService.add({
        summary: `${player.name} successfully removed from campaign`,
        severity: 'success'
      });
    });
  }

  removeInvitation(player: Player) {
    this.service.removeInvitation(this.entity.id, player.id).subscribe(() => {
      this.invitations.splice(this.invitations.findIndex(p => p.player.id === player.id), 1);
      this.messageService.add({
        summary: `${player.name} invitation successfully removed`,
        severity: 'success'
      });
    });
  }
  removeHero(hero: Hero) {
    this.service.removeHero(this.entity.id, hero.id).subscribe(() => {
      this.entity.heroes.splice(this.entity.heroes.findIndex(h => h.id === hero.id), 1);
      this.messageService.add({
        summary: `${hero.name} successfully removed from campaign`,
        severity: 'success'
      });
    });
  }

  hasHero() {
    return this.entity.heroes.some(h => h.ownerId === this.authenticationService.userId);
  }

  addHero() {
    this.dialogService.open(HeroesSelectModalComponent, {
      data: {
        campaignId: this.entity.id
      }
    }).onClose.subscribe((hero: Hero) => {
      if (hero) {
        this.service.addHero(this.entity.id, hero.id)
          .subscribe(() => {
            this.entity.heroes.push(hero);
            this.messageService.add({
              summary: `${hero.name} successfully added to campaign`,
              severity: 'success'
            });
          });
      }
    });
  }

  isHeroOwner(id: string) {
    return this.authenticationService.userId === id;
  }

  startSession() {
    this.router.navigate([`/campaign-session/${this.entity.id}`]);
    this.ref.close();
  }

  private async getInvitations() {
    return this.action === EditorAction.create ? of<CampaignInvitationsOutput[]>([]).toPromise() : this.service.getCampaignInvitations(this.entityId);
  }
}
