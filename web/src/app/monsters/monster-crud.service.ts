import {Injectable, Injector} from '@angular/core';
import {BaseCrudService} from '../shared/base-service/base-crud-service';
import {Monster} from '../shared/models/Monster.model';
import {RRColumns} from '../shared/components/cm-grid/cm-grid.component';

@Injectable({
  providedIn: 'root'
})
export class MonsterCrudService extends BaseCrudService<Monster> {
  fieldName = 'name';
  modalSelectColumns: RRColumns[] = [
    {
      header: 'Monster',
      property: 'name'
    }
  ];
  selectModalTitle = 'Monsters';
  selectPlaceholder = 'Monsters';
  path = 'monsters';

  constructor(
    injector: Injector
  ) {
    super(injector);
  }
}
