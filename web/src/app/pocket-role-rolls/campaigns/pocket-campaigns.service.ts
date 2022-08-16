import { Injectable, Injector } from '@angular/core';
import { Observable, of } from 'rxjs';
import { LOH_API } from 'src/app/loh.api';
import { BaseCrudService } from 'src/app/shared/base-service/base-crud-service';
import { RRColumns } from 'src/app/shared/components/cm-grid/cm-grid.component';
import { PocketCampaignModel } from 'src/app/shared/models/pocket/campaigns/pocket.campaign.model';
import { v4 as uuidv4 } from 'uuid';

@Injectable({
  providedIn: 'root'
})
export class PocketCampaignsService extends BaseCrudService<PocketCampaignModel, PocketCampaignModel> {
  public path = 'campaigns';
  public selectPlaceholder: string;
  public fieldName: string;
  public selectModalTitle: string;
  public selectModalColumns: RRColumns[];
  public entityListColumns: RRColumns[];
  public serverUrl = LOH_API.myPocketBackUrl;
  constructor(
    injector: Injector,
  ) {
    super(injector);
   }

   override getNew() :Observable<PocketCampaignModel> {
      return of<PocketCampaignModel>({
        id: uuidv4(),
        name: 'New Campaign'
      } as PocketCampaignModel);
   }
}

