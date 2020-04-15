import {Injectable, Injector} from '@angular/core';
import {BaseEntityService} from 'src/app/shared/base-entity-service';
import {WeaponModel} from 'src/app/shared/models/WeaponModel.model';

@Injectable({
  providedIn: 'root'
})
export class WeaponModelService extends BaseEntityService<WeaponModel> {

  path = 'weaponModels';

  constructor(
    injector: Injector
  ) {
    super(injector, WeaponModel);
   }
}
