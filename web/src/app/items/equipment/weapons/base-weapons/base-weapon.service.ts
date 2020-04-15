import {Injectable, Injector} from '@angular/core';
import {BaseWeapon} from 'src/app/shared/models/BaseWeapon.model';
import {BaseEntityService} from 'src/app/shared/base-entity-service';

@Injectable({
  providedIn: 'root'
})
export class BaseWeaponService extends BaseEntityService<BaseWeapon> {

  path = 'baseWeapons';

  constructor(
    injector: Injector
  ) {
    super(injector, BaseWeapon);
   }
}
