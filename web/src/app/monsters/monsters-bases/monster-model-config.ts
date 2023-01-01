import {MonsterModelComponent} from './monster-model-editor/monster-model.component';
import {BaseComponentConfig} from '../../shared/components/base-component-config';
import {RRColumns} from '../../shared/components/cm-grid/cm-grid.component';
import {DynamicDialogConfig} from 'primeng/dynamicdialog';
import { MonsterModel } from 'src/app/shared/models/creatures/monsters/MonsterModel.model';
import { RRAction } from 'src/app/shared/components/rr-grid/r-r-grid.component';

export class MonsterModelConfig implements BaseComponentConfig<MonsterModel> {
  entityListActions: RRAction<MonsterModel>[] = [];
  navigateUrlOnRowSelect: string = null;
  navigateOnRowSelect = false;
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
