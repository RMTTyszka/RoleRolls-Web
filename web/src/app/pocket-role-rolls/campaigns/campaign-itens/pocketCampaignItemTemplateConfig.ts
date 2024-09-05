import { Type } from "@angular/core";
import { DynamicDialogConfig } from "primeng/dynamicdialog";
import { BaseComponentConfig } from "src/app/shared/components/base-component-config";
import { RRAction, RRColumns } from "src/app/shared/components/rr-grid/r-r-grid.component";
import { PocketCampaignModel } from "src/app/shared/models/pocket/campaigns/pocket.campaign.model";
import { CampaignCreatorComponent } from "./campaign-creator/campaign-creator.component";
import {CampaignEditorBodyShellComponent} from "./campaign-editor-body-shell/campaign-editor-body-shell.component";

export class PocketCampaignItemTemplateConfig implements BaseComponentConfig<PocketCampaignModel> {
  entityListActions: RRAction<PocketCampaignModel>[] = [];

  editor = CampaignEditorBodyShellComponent;
  creator = CampaignEditorBodyShellComponent;
  creatorOptions: DynamicDialogConfig;
  path = 'item-templates';
  selectPlaceholder = 'Item Template';
  fieldName: 'name';
  editorTitle: 'Item Template';
  selectModalTitle: 'Item Template';
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
  navigateUrlOnRowSelect: 'item-templates/';
  navigateOnRowSelect: boolean;
}
