import {Type} from '@angular/core';
import {RRAction, RRColumns} from './rr-grid/r-r-grid.component';
import {DynamicDialogConfig} from 'primeng/dynamicdialog';
import { Entity } from '../models/Entity.model';

export interface BaseComponentConfig<T extends Entity> {
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
  entityListActions: RRAction<T>[];
  navigateUrlOnRowSelect: string;
  navigateOnRowSelect: boolean;
}
