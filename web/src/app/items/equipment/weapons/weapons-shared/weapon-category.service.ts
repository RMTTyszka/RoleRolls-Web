import { Injectable, Injector } from '@angular/core';
import { BaseEntityService } from 'src/app/shared/base-entity-service';
import { WeaponCategory } from 'src/app/shared/models/WeaponCategory.model';

@Injectable({
  providedIn: 'root'
})
export class WeaponCategoryService extends BaseEntityService<WeaponCategory> {
  path =  'weaponCategory';
  constructor(
    injector: Injector
  ) {
    super(injector, WeaponCategory);
  }
}
