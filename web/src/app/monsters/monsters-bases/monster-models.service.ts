import {Injectable, Injector} from '@angular/core';
import {BaseCrudService} from '../../shared/base-service/base-crud-service';
import {MonsterModel} from '../../shared/models/MonsterModel.model';
import {RRColumns} from '../../shared/components/cm-grid/cm-grid.component';

@Injectable({
  providedIn: 'root'
})
export class MonsterModelsService extends BaseCrudService<MonsterModel> {
  fieldName = 'name';
  modalSelectColumns: RRColumns[] = [
    {
      header: 'Name',
      property: 'Name'
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
