import {Type} from '@angular/core';
import {RRColumns} from './cm-grid/cm-grid.component';

export interface BaseComponentConfig {
  editor: Type<any>;
  path: string;
  selectPlaceholder: string;
  fieldName: string;
  selectModalTitle: string;
  selectModalColumns: RRColumns[];
  entityListColumns: RRColumns[];
}
