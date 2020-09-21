import {Injectable, Injector} from '@angular/core';
import {BaseCrudService} from '../shared/base-service/base-crud-service';
import {Monster} from '../shared/models/Monster.model';

@Injectable({
  providedIn: 'root'
})
export class MonsterCrudService extends BaseCrudService<Monster> {
  path = 'monsters';

  constructor(
    injector: Injector
  ) {
    super(injector);
  }
}
