import { Component, EventEmitter, TemplateRef, ViewChild } from '@angular/core';
import { Dialog } from 'primeng/dialog';
import { Popover } from 'primeng/popover';
import { AuthenticationService } from '../../authentication/services/authentication.service';
import { ConfirmationService } from 'primeng/api';
import { Router } from '@angular/router';
import {finalize, Observable} from 'rxjs';
import { Campaign } from '../models/campaign';
import { RRAction } from '../../models/RROption';
import { CampaignsService } from '../services/campaigns.service';
import { FormsModule } from '@angular/forms';
import { CdkCopyToClipboard } from '@angular/cdk/clipboard';
import { GridComponent, RRColumns, RRHeaderAction } from '../../components/grid/grid.component';
import { safeCast } from '../../tokens/utils.funcs';
import { CampaignView } from '@app/models/campaigns/campaign-view';
import {GetListInput} from '@app/tokens/get-list-input';
import {PagedOutput} from '@app/models/PagedOutput';

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
  public rowActions: RRAction<CampaignView>[] = [];
  public refreshGrid = new EventEmitter<void>();
  public rowSelected = (campaign: CampaignView) => this.toCampaignDetails(campaign);
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
      } as RRColumns,
      {
        header: 'Template',
        property: 'templateName'
      } as RRColumns,
    ];
    this.rowActions.push(
      {
        icon: 'pi pi-arrow-circle-right',
        callBack: ((entity: CampaignView) => {
          this.router.navigate([`pocket/campaigns/${entity.id}`]);
        }),
        condition: ((entity: CampaignView) => {
          return true;
        }),
        csClass: null,
        tooltip: 'Start Session'
      },
      {
        icon: 'pi pi-times-circle',
        callBack: ((entity: CampaignView) => {
          this.delete(entity);
        }),
        condition: ((entity: CampaignView) => {
          return this.isMaster(entity.masterId);
        }),
        csClass: 'p-button-danger',
        tooltip: 'Remove'
      },
      {
        icon: 'pi pi-plus',
        callBack: ((entity: CampaignView, target: any) => {
          this.invitePlayer(entity, target);
        }),
        condition: ((entity: CampaignView) => {
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
        callBack: () => this.toCampaignDetails({} as CampaignView),
        icon: 'pi pi-plus',
        condition: () => true,
        tooltip: 'Create',
        csClass: null,
      } as RRHeaderAction,
    ];
    this.rowActions.push(
      {
        icon: 'pi pi-arrow-circle-right',
        callBack: ((entity: CampaignView) => {
          this.router.navigate([`pocket/campaigns/${entity.id}`]);
        }),
        condition: ((entity: CampaignView) => {
          return true;
        }),
        csClass: null,
        tooltip: 'Start Session'
      },
      {
        icon: 'pi pi-times-circle',
        callBack: ((entity: CampaignView) => {
          this.delete(entity);
        }),
        condition: ((entity: CampaignView) => {
          return this.isMaster(entity.masterId);
        }),
        csClass: 'p-button-danger',
        tooltip: 'Remove'
      },
      {
        icon: 'pi pi-plus',
        callBack: ((entity: CampaignView, target: any) => {
          this.invitePlayer(entity, target);
        }),
        condition: ((entity: CampaignView) => {
          return this.isMaster(entity.masterId);
        }),
        csClass: null,
        tooltip: 'Invite'
      },
    );
  }

  ngOnInit(): void {
  }
  getList = (input: GetListInput) => {
    return this.service.getList(input);
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
  private invitePlayer(entity: CampaignView, target: any) {
    this.service.invitePlayer(entity.id).subscribe((code: string) => {
      this.invitationCode = code;
      this.invitationCodeOverlay.toggle(event, target);
    });
  }
  private isMaster(userId: string): boolean {
    return this.authenticationService.userId === userId;
  }
  private delete(entity: CampaignView): void {
    this.confirmationService.confirm({
      accept: () => this.service.delete(entity.id).subscribe(() => {}),
      header: 'Confirm delete?'
    });
  }

  private toCampaignDetails(campaign: CampaignView) {
    const id = campaign.id ?? 'new';
    this.router.navigate([`campaigns/${id}`]);
  }
}
