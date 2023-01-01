import {BaseComponentConfig} from '../shared/components/base-component-config';
import {RRAction, RRColumns} from '../shared/components/rr-grid/r-r-grid.component';
import {RolesEditorComponent} from './roles-editor/roles-editor.component';
import {DynamicDialogConfig} from 'primeng/dynamicdialog';
import { Role } from '../shared/models/Role.model';

export class RoleConfig implements BaseComponentConfig<Role> {
  entityListActions: RRAction<Role>[] = [];
  navigateUrlOnRowSelect: string;
  navigateOnRowSelect: boolean;
  creatorOptions: DynamicDialogConfig = null;
  editorTitle = 'Role';
  editor = RolesEditorComponent;
  creator = RolesEditorComponent;
  entityListColumns: RRColumns[] = [
    {
      header: 'Role',
      property: 'name'
    }
  ];
  fieldName: string;
  selectModalColumns: RRColumns[] = [
    {
      header: 'Role',
      property: 'name'
    }
  ];
  selectModalTitle = 'Role';
  selectPlaceholder = 'Role';
  path = 'roles';
}
