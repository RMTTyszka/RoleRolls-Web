import {ArmorModel} from './Armor.model';
import {Entity} from './Entity.model';
import {ArmorInstance} from './ArmorInstance.model';

export class Equipment extends Entity {
  armorInstance: ArmorInstance = null;
}
