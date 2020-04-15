import {Injectable, Injector} from '@angular/core';
import {BaseEntityService} from 'src/app/shared/base-entity-service';
import {BaseArmor} from 'src/app/shared/models/BaseArmor.model';

@Injectable({
  providedIn: 'root'
})
export class BaseArmorService extends BaseEntityService<BaseArmor> {

  path = 'baseArmors';

  constructor(
    injector: Injector
  ) {
    super(injector, BaseArmor);
   }
}
