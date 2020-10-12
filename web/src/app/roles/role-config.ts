import {BaseComponentConfig} from '../shared/components/base-component-config';
import {RRColumns} from '../shared/components/rr-grid/r-r-grid.component';
import {RolesEditorComponent} from './roles-editor/roles-editor.component';

export class RoleConfig implements BaseComponentConfig {
  editorTitle = 'Role';
  editor = RolesEditorComponent;
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
