import {Injectable, Injector} from '@angular/core';
import {BaseEntityService} from 'src/app/shared/base-entity-service';
import {Monster} from 'src/app/shared/models/Monster.model';
import {BaseCrudService} from '../../shared/base-service/base-crud-service';
import {RRColumns} from '../../shared/components/cm-grid/cm-grid.component';

@Injectable({
  providedIn: 'root'
})
export class MonsterService extends BaseCrudService<Monster> {
  fieldName: string;
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
