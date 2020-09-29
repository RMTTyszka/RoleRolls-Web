import {Injectable, Injector} from '@angular/core';
import {Monster} from '../shared/models/creatures/monsters/Monster.model';
import {BaseEntityService} from '../shared/base-entity-service';


@Injectable({
  providedIn: 'root'
})
export class MonstersService extends BaseEntityService<Monster> {
  path = 'monsters';
  constructor(

    injector: Injector
  ) {
    super(injector, Monster);
   }
}
