import {BaseComponentConfig} from '../shared/components/base-component-config';
import {RRColumns} from '../shared/components/rr-grid/r-r-grid.component';
import {CampaignEditorComponent} from './campaign-editor/campaign-editor.component';
import {DynamicDialogConfig} from 'primeng';

export class CampaingsConfig implements BaseComponentConfig {
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
