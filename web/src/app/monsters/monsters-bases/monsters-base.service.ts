import { Injectable, Injector } from '@angular/core';
import { BaseCrudServiceComponent } from 'src/app/shared/base-service/base-crud-service.component';
import { MonsterModel } from 'src/app/shared/models/MonsterModel.model';

@Injectable({
  providedIn: 'root'
})
export class MonstersBaseService extends BaseCrudServiceComponent<MonsterModel> {
  path = 'monsterBase';
  constructor(injector: Injector) {
    super(injector);
   }
}
