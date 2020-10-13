import {Type} from '@angular/core';
import {RRColumns} from './rr-grid/r-r-grid.component';

export interface BaseComponentConfig {
  editor: Type<any>;
  creator: Type<any>;
  path: string;
  selectPlaceholder: string;
  fieldName: string;
  editorTitle: string;
  selectModalTitle: string;
  selectModalColumns: RRColumns[];
  entityListColumns: RRColumns[];
}
