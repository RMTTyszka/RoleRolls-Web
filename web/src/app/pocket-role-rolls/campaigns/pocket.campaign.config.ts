import { Type } from "@angular/core";
import { DynamicDialogConfig } from "primeng/dynamicdialog";
import { BaseComponentConfig } from "src/app/shared/components/base-component-config";
import { RRColumns } from "src/app/shared/components/rr-grid/r-r-grid.component";

export class PocketCampaignConfig implements BaseComponentConfig {

  editor: Type<any>;
  creator: Type<any>;
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
