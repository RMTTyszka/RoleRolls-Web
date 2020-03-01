import {Entity} from './Entity.model';
import {ArmorInstance} from './ArmorInstance.model';
import { WeaponInstance } from './WeaponInstance.model';

export class Equipment extends Entity {
  armor: ArmorInstance = null;
  mainWeapon: WeaponInstance = new WeaponInstance();
}
