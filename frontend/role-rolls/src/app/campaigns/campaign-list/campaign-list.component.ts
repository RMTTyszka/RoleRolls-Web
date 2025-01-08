import { Component, TemplateRef, ViewChild } from '@angular/core';
import { Dialog } from 'primeng/dialog';
import { Popover } from 'primeng/popover';
import { AuthenticationService } from '../../authentication/services/authentication.service';
import { ConfirmationService } from 'primeng/api';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { OverlayPanel } from 'primeng/overlaypanel';
import { Campaign } from '../models/campaign';
import { RRAction } from '../../models/RROption';
import { RRAction } from '../../models/RROption';
import EventEmitter from 'node:events';
import { PocketCampaignsService } from '../services/pocket-campaigns.service';

@Component({
  selector: 'rr-campaign-list',
  imports: [
    Dialog,
    Popover
  ],
  templateUrl: './campaign-list.component.html',
  styleUrl: './campaign-list.component.scss'
})
export class CampaignListComponent {
  @ViewChild('invitationCodeOverlay') public invitationCodeOverlay!: Popover;
  @ViewChild('invitationButton') public invitationButton!: TemplateRef<any>;

  public displayInsertInvitationCode: boolean;
  public invitationCode: string;
  public actions: RRAction<void>[] = [];
  public refreshGrid = new EventEmitter<void>();
  constructor(
    public service: PocketCampaignsService,
    private readonly authenticationService: AuthenticationService,
    private readonly confirmationService: ConfirmationService,
    public router: Router,
  ) {
    this.config.entityListActions.push(
      {
        icon: 'pi pi-arrow-circle-right',
        callBack: ((entity: Campaign) => {
          this.router.navigate([`pocket/campaigns/${entity.id}`]);
        }),
        condition: ((entity: Campaign) => {
          return true;
        }),
        csClass: null,
        tooltip: 'Start Session'
      },
      {
        icon: 'pi pi-times-circle',
        callBack: ((entity: Campaign) => {
          this.delete(entity);
        }),
        condition: ((entity: Campaign) => {
          return this.isMaster(entity.masterId);
        }),
        csClass: 'p-button-danger',
        tooltip: 'Remove'
      },
      {
        icon: 'pi pi-plus',
        callBack: ((entity: Campaign, target: any) => {
          this.invitePlayer(entity, target);
        }),
        condition: ((entity: Campaign) => {
          return this.isMaster(entity.masterId);
        }),
        csClass: null,
        tooltip: 'Invite'
      },
    );
    this.actions = [
      {
        callBack: () => this.toggleAcceptInvitation(),
        icon: 'pi pi-thumbs-up',
        condition: () => true,
        tooltip: 'Accept Invitation',
        csClass: null,
      } as RRAction<void>,
      /*        {
                callBack: () => this.importTemplate(),
                icon: 'pi pi-download',
                condition: () => true,
                tooltip: 'Import Default Universe',
                csClass: null
              } as RRAction<void>,*/
    ];
  }

  ngOnInit(): void {
  }
  public toggleAcceptInvitation() {
    this.displayInsertInvitationCode = true;
  }
  public openAcceptInvitation() {
    this.displayInsertInvitationCode = true;
  }
  public acceptInvitation() {
    this.service.acceptInvitation(this.invitationCode)
      .pipe(finalize(() => {
        this.displayInsertInvitationCode = false;
        this.invitationCode = null;
      })).subscribe(() => {
      this.refreshGrid.next();
    });
  }
  private invitePlayer(entity: Campaign, target: any) {
    this.service.invitePlayer(entity.id).subscribe((code: string) => {
      this.invitationCode = code;
      this.invitationCodeOverlay.toggle(event, target);
    });
  }
  private isMaster(userId: string): boolean {
    return this.authenticationService.userId === userId;
  }
  private delete(entity: Campaign): void {
    this.confirmationService.confirm({
      accept: () => this.service.delete(entity.id).subscribe(() => this.service.entityDeleted.next(entity)),
      header: 'Confirm delete?'
    });
  }
}
