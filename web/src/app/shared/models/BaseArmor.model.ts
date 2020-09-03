import {ArmorCategory} from './items/ArmorCategory.model';
import {Entity} from './Entity.model';

export class BaseArmor extends Entity {
  static = false;
  name = '';
  category: ArmorCategory = new ArmorCategory();
}
