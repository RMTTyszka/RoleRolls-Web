import {BaseComponentConfig} from '../shared/components/base-component-config';
import {MonsterComponent} from './monster/monster.component';
import {RRColumns} from '../shared/components/rr-grid/r-r-grid.component';
import {MonsterCreateComponent} from './monster-create/monster-create.component';
import {DynamicDialogConfig} from 'primeng';

export class MonsterConfig implements BaseComponentConfig {
  creatorOptions: DynamicDialogConfig;
  editorTitle = 'Monsters';
  editor = MonsterComponent;
  creator = MonsterCreateComponent;
  entityListColumns: RRColumns[] = [
    {
      header: 'Monster',
      property: 'name'
    }
  ];
  fieldName: string;
  selectModalColumns: RRColumns[] = [
    {
      header: 'Monster',
      property: 'name'
    }
  ];
  selectModalTitle = 'Monsters';
  selectPlaceholder = 'Monsters';
  path = 'monsters';
}
