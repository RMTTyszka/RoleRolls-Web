import {Injectable, Injector} from '@angular/core';
import {BaseCrudServiceComponent} from '../shared/base-service/base-crud-service.component';
import {Monster} from '../shared/models/Monster.model';


@Injectable({
  providedIn: 'root'
})
export class MonstersService extends BaseCrudServiceComponent<Monster> {
  path = 'monsters';
  constructor(

    injector: Injector
  ) {
    super(injector);
   }
}
