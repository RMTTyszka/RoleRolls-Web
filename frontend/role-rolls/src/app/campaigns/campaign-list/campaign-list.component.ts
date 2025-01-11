import { Component, EventEmitter, TemplateRef, ViewChild } from '@angular/core';
import { Dialog } from 'primeng/dialog';
import { Popover } from 'primeng/popover';
import { AuthenticationService } from '../../authentication/services/authentication.service';
import { ConfirmationService } from 'primeng/api';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { Campaign } from '../models/campaign';
import { RRAction } from '../../models/RROption';
import { CampaignsService } from '../services/campaigns.service';
import { FormsModule } from '@angular/forms';
import { CdkCopyToClipboard } from '@angular/cdk/clipboard';
import { GridComponent, RRColumns, RRHeaderAction } from '../../components/grid/grid.component';
import { safeCast } from '../../tokens/utils.funcs';

@Component({
  selector: 'rr-campaign-list',
  imports: [
    Dialog,
    Popover,
    FormsModule,
    CdkCopyToClipboard,
    GridComponent
  ],
  templateUrl: './campaign-list.component.html',
  styleUrl: './campaign-list.component.scss'
})
export class CampaignListComponent {
  @ViewChild('invitationCodeOverlay') public invitationCodeOverlay!: Popover;
  @ViewChild('invitationButton') public invitationButton!: TemplateRef<any>;

  public displayInsertInvitationCode: boolean = false;
  public invitationCode: string = '';
  public headerActions: RRHeaderAction[] = [];
  public rowActions: RRAction<Campaign>[] = [];
  public refreshGrid = new EventEmitter<void>();
  public rowSelected = (campaign: Campaign) => this.toCampaignDetails(campaign);
  public columns: RRColumns[] = [];
  constructor(
    public service: CampaignsService,
    private readonly authenticationService: AuthenticationService,
    private readonly confirmationService: ConfirmationService,
    public router: Router,
  ) {
    this.columns = [
      {
        header: 'Name',
        property: 'name'
      } as RRColumns
    ];
    this.rowActions.push(
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
    this.headerActions = [
      {
        callBack: () => this.toggleAcceptInvitation(),
        icon: 'pi pi-thumbs-up',
        condition: () => true,
        tooltip: 'Accept Invitation',
        csClass: null,
      } as RRHeaderAction,
      {
        callBack: () => this.toCampaignDetails({} as Campaign),
        icon: 'pi pi-plus',
        condition: () => true,
        tooltip: 'Create',
        csClass: null,
      } as RRHeaderAction,
    ];
    this.rowActions.push(
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
    this.service.acceptInvitation(safeCast<string>(this.invitationCode))
      .pipe(finalize(() => {
        this.displayInsertInvitationCode = false;
        this.invitationCode = '';
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
      accept: () => this.service.delete(entity.id).subscribe(() => {}),
      header: 'Confirm delete?'
    });
  }

  private toCampaignDetails(campaign: Campaign) {
    const id = campaign.id ?? 'new';
    this.router.navigate([`campaigns/${id}`]);
  }
}
