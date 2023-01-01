import { Type } from "@angular/core";
import { DynamicDialogConfig } from "primeng/dynamicdialog";
import { BaseComponentConfig } from "src/app/shared/components/base-component-config";
import { RRAction, RRColumns } from "src/app/shared/components/rr-grid/r-r-grid.component";
import { PocketCampaignModel } from "src/app/shared/models/pocket/campaigns/pocket.campaign.model";
import { CampaignCreatorComponent } from "./campaign-creator/campaign-creator.component";

export class PocketCampaignConfig implements BaseComponentConfig<PocketCampaignModel> {
  entityListActions: RRAction<PocketCampaignModel>[] = [];

  editor = CampaignCreatorComponent;
  creator = CampaignCreatorComponent;
  creatorOptions: DynamicDialogConfig;
  path = 'campaigns';
  selectPlaceholder = 'Campaigns';
  fieldName: 'name';
  editorTitle: 'Campaigns';
  selectModalTitle: 'Campaigns';
  selectModalColumns: RRColumns[] = [
    {
      header: 'Name',
      property: 'name'
    } as RRColumns
  ];
  entityListColumns: RRColumns[] = [
    {
      header: 'Name',
      property: 'name'
    } as RRColumns
  ];
  navigateUrlOnRowSelect: 'campaigns/';
  navigateOnRowSelect: boolean;
}
