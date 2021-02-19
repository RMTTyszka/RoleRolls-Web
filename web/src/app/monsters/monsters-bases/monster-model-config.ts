import {MonsterModelComponent} from './monster-model-editor/monster-model.component';
import {BaseComponentConfig} from '../../shared/components/base-component-config';
import {RRColumns} from '../../shared/components/cm-grid/cm-grid.component';
import {DynamicDialogConfig} from 'primeng';

export class MonsterModelConfig implements BaseComponentConfig {
  creatorOptions: DynamicDialogConfig;
  editorTitle = 'Monster Model';
  entityListColumns: RRColumns[] = [
    {
      header: 'Name',
      property: 'name'
    }
  ];
  fieldName = 'name';
  selectModalColumns: RRColumns[] = [
    {
      header: 'Name',
      property: 'name'
    }
  ];
  path = 'monsters/model';
  selectModalTitle = 'Monster Model';
  selectPlaceholder = 'Monster Model';
  editor = MonsterModelComponent;
  creator = MonsterModelComponent;
}
