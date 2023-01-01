import {BaseComponentConfig} from '../shared/components/base-component-config';
import {RRAction, RRColumns} from '../shared/components/rr-grid/r-r-grid.component';
import {CampaignEditorComponent} from './campaign-editor/campaign-editor.component';
import {DynamicDialogConfig} from 'primeng/dynamicdialog';
import { Campaign } from '../shared/models/campaign/Campaign.model';

export class CampaingsConfig implements BaseComponentConfig<Campaign> {
  entityListActions: RRAction<Campaign>[] = [];
  navigateUrlOnRowSelect: string = null;
  navigateOnRowSelect = false;
  creatorOptions: DynamicDialogConfig;
  editorTitle = 'Campaign';
  editor = CampaignEditorComponent;
  creator = CampaignEditorComponent;
  entityListColumns: RRColumns[] = [
    {
      header: 'Name',
      property: 'name'
    },
    {
      header: 'Resume',
      property: 'description'
    },
  ];
  fieldName: 'name';
  path: 'campaigns';
  selectModalColumns: RRColumns[] = this.entityListColumns;
  selectModalTitle = 'Campaign';
  selectPlaceholder: 'Campaign';

}
