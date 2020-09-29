import {BaseComponentConfig} from '../shared/components/base-component-config';
import {MonsterComponent} from './monster/monster.component';
import {RRColumns} from '../shared/components/rr-grid/r-r-grid.component';

export class MonsterConfig implements BaseComponentConfig {
  editor = MonsterComponent;
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
