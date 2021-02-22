import {Injectable, Injector} from '@angular/core';
import {WeaponCategory} from 'src/app/shared/models/WeaponCategory.model';
import {of} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WeaponCategoryService {
  constructor(
    injector: Injector
  ) {
  }
  getAll() {
    return of<WeaponCategory[]>(<WeaponCategory[]>[
      WeaponCategory.None, WeaponCategory.Light, WeaponCategory.Medium, WeaponCategory.Heavy, WeaponCategory.Shield
    ]);
  }
}
