import { Injectable, Injector } from '@angular/core';
import { BaseEntityService } from 'src/app/shared/base-entity-service';
import { Monster } from 'src/app/shared/models/Monster.model';

@Injectable({
  providedIn: 'root'
})
export class MonsterService extends BaseEntityService<Monster> {
  path = 'monsters';
  constructor(
    injector: Injector
    ) {
    super(injector, Monster);
   }
}
