import { ConfirmationService } from 'primeng/api';
import { AuthenticationService } from './../../../authentication/authentication.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseComponentConfig } from 'src/app/shared/components/base-component-config';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { PocketCampaignsService } from '../pocket-campaigns.service';
import { PocketCampaignConfig } from '../pocket.campaign.config';

@Component({
  selector: 'rr-pocket-campaigns',
  templateUrl: './pocket-campaigns.component.html',
  styleUrls: ['./pocket-campaigns.component.scss']
})
export class PocketCampaignsComponent implements OnInit {


  public config = new PocketCampaignConfig();
  constructor(
    public service: PocketCampaignsService,
    private readonly authenticationService: AuthenticationService,
    private readonly confirmationService: ConfirmationService,
    public router: Router,
    ) 
    {
      this.config.entityListActions.push(
        {
          icon: 'pi pi-arrow-circle-right',
          callBack: ((entity: PocketCampaignModel) => {
            this.router.navigate([`pocket/campaigns/${entity.id}`]);
          }),
          condition: ((entity: PocketCampaignModel) => {
            return true;
          }),
          csClass: null
        },
        {
          icon: 'pi pi-times-circle',
          callBack: ((entity: PocketCampaignModel) => {
            this.delete(entity);
          }),
          condition: ((entity: PocketCampaignModel) => {
            return this.isMaster(entity.masterId);
          }),
          csClass: 'p-button-danger'
        },
      )
    };

  ngOnInit(): void {
  }
  private isMaster(userId: string): boolean {
    return this.authenticationService.userId === userId;
  }
  private delete(entity: PocketCampaignModel): void {
    this.confirmationService.confirm({
      accept: () => this.service.delete(entity.id).subscribe(() => this.service.entityDeleted.next(entity)),
      header: 'Confirm delete?'
    })
  }

}
