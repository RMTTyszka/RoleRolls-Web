import {Injectable, Injector} from '@angular/core';
import {BaseCrudService} from '../../shared/base-service/base-crud-service';
import {MonsterModel} from '../../shared/models/creatures/monsters/MonsterModel.model';
import {RRColumns} from '../../shared/components/cm-grid/cm-grid.component';
import {MonsterModelComponent} from './monster-model-editor/monster-model.component';
import {MonsterModelConfig} from './monster-model-config';

@Injectable({
  providedIn: 'root'
})
export class MonsterModelsService extends BaseCrudService<MonsterModel, MonsterModel> {
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
  constructor(
    injector: Injector
  ) {
    super(injector);
  }
}
