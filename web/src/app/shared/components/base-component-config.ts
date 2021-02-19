import {Type} from '@angular/core';
import {RRColumns} from './rr-grid/r-r-grid.component';
import {DynamicDialogConfig} from 'primeng';

export interface BaseComponentConfig {
  editor: Type<any>;
  creator: Type<any>;
  creatorOptions: DynamicDialogConfig;
  path: string;
  selectPlaceholder: string;
  fieldName: string;
  editorTitle: string;
  selectModalTitle: string;
  selectModalColumns: RRColumns[];
  entityListColumns: RRColumns[];
}
