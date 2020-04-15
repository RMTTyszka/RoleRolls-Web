import {Injectable, Injector} from '@angular/core';
import {MonsterModel} from 'src/app/shared/models/MonsterModel.model';
import {BaseEntityService} from 'src/app/shared/base-entity-service';

@Injectable({
  providedIn: 'root'
})
export class MonsterBaseService extends BaseEntityService<MonsterModel> {
  path = 'monsterBase';
  constructor(
    injector: Injector,
  ) {
    super(injector, MonsterModel);
   }
}
