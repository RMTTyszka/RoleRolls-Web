import {BaseComponentConfig} from '../shared/components/base-component-config';
import {RRColumns} from '../shared/components/rr-grid/r-r-grid.component';
import {DynamicDialogConfig} from 'primeng/dynamicdialog';

export class ItemTemplateConfig implements BaseComponentConfig {
  navigateUrlOnRowSelect: string = null;
  navigateOnRowSelect = false;
  creatorOptions: DynamicDialogConfig = null;
  editorTitle = 'Item Template';
  editor = null;
  creator = null;
  entityListColumns: RRColumns[] = [
    {
      header: 'Item',
      property: 'name'
    }
  ];
  fieldName = 'name';
  selectModalColumns: RRColumns[] = [
    {
      header: 'Item',
      property: 'name'
    }
  ];
  selectModalTitle = 'Item Templates';
  selectPlaceholder = 'Item Templates';
  path = 'item-templates';
}
