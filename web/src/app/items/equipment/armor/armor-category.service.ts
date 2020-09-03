import {Injectable, Injector} from '@angular/core';
import {BaseEntityService} from '../../../shared/base-entity-service';
import {ArmorCategory} from '../../../shared/models/items/ArmorCategory.model';

@Injectable({
  providedIn: 'root'
})
export class ArmorCategoryService extends BaseEntityService<ArmorCategory> {
  path =  'armorCategory';
  constructor(
    injector: Injector
  ) {
    super(injector, ArmorCategory);
  }
}
