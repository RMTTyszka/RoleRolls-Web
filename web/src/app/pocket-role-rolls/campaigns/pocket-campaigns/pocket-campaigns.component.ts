import { ConfirmationService } from 'primeng/api';
import { AuthenticationService } from './../../../authentication/authentication.service';
import { Component, EventEmitter, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { BaseComponentConfig } from 'src/app/shared/components/base-component-config';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { PocketCampaignsService } from '../pocket-campaigns.service';
import { PocketCampaignConfig } from '../pocket.campaign.config';
import { OverlayPanel } from 'primeng/overlaypanel';
import { finalize } from 'rxjs/operators';
import { RRAction } from 'src/app/shared/components/rr-grid/r-r-grid.component';
import {DialogService} from 'primeng/dynamicdialog';
import {TemplateSelectorComponent} from '../CampaignEditor/template-selector/template-selector.component';
import { firstValueFrom } from 'rxjs';
import {TemplateImportInput} from '../models/TemplateImportInput';
import {EditorAction} from '../../../shared/dtos/ModalEntityData';

@Component({
  selector: 'rr-pocket-campaigns',
  templateUrl: './pocket-campaigns.component.html',
  styleUrls: ['./pocket-campaigns.component.scss']
})
export class PocketCampaignsComponent implements OnInit {
  @ViewChild('invitationCodeOverlay') public invitationCodeOverlay: OverlayPanel;
  @ViewChild('invitationButton') public invitationButton: TemplateRef<any>;

  public displayInsertInvitationCode: boolean;
  public invitationCode: string;
  public config = new PocketCampaignConfig();
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
          callBack: ((entity: PocketCampaignModel) => {
            this.router.navigate([`pocket/campaigns/${entity.id}`]);
          }),
          condition: ((entity: PocketCampaignModel) => {
            return true;
          }),
          csClass: null,
          tooltip: 'Start Session'
        },
        {
          icon: 'pi pi-times-circle',
          callBack: ((entity: PocketCampaignModel) => {
            this.delete(entity);
          }),
          condition: ((entity: PocketCampaignModel) => {
            return this.isMaster(entity.masterId);
          }),
          csClass: 'p-button-danger',
          tooltip: 'Remove'
        },
        {
          icon: 'pi pi-plus',
          callBack: ((entity: PocketCampaignModel, target: any) => {
            this.invitePlayer(entity, target);
          }),
          condition: ((entity: PocketCampaignModel) => {
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
          csClass: null
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
  private invitePlayer(entity: PocketCampaignModel, target: any) {
    this.service.invitePlayer(entity.id).subscribe((code: string) => {
      this.invitationCode = code;
      this.invitationCodeOverlay.toggle(event, target);
    });
  }
  private isMaster(userId: string): boolean {
    return this.authenticationService.userId === userId;
  }
  private delete(entity: PocketCampaignModel): void {
    this.confirmationService.confirm({
      accept: () => this.service.delete(entity.id).subscribe(() => this.service.entityDeleted.next(entity)),
      header: 'Confirm delete?'
    });
  }

/*  private async importTemplate() {
    const templateImport = await firstValueFrom<TemplateImportInput>(this.dialogService.open(TemplateSelectorComponent, {}).onClose);
    if (templateImport) {
      await firstValueFrom(this.service.importTemplate(templateImport));
      this.dialogService.open(this.config.editor, {
        data: {
          entityId: templateImport.id,
          service: this.service,
          action: EditorAction.update
        },
        width: '100vw',
        height: '100vh',
        header: this.config.editorTitle
      });
    }
  }*/
}
