import {BaseComponentConfig} from '../shared/components/base-component-config';
import {NewHeroEditorComponent} from './new-hero-editor/new-hero-editor.component';
import {RRColumns} from '../shared/components/rr-grid/r-r-grid.component';

export class HeroConfig implements BaseComponentConfig {
  editorTitle = 'Hero';
  editor = NewHeroEditorComponent;
  entityListColumns: RRColumns[] = [
    {
      header: 'Name',
      property: 'name'
    },
    {
      header: 'Race',
      property: 'race.name'
    },
    {
      header: 'Role',
      property: 'role.role'
    },
  ];
  fieldName: 'name';
  path: 'heroes';
  selectModalColumns: RRColumns[] = this.entityListColumns;
  selectModalTitle = 'Hero';
  selectPlaceholder: 'Hero';

}
