import {Equipable} from './Equipable.model';
import {BaseWeapon} from './BaseWeapon.model';

export class WeaponModel extends Equipable {
  baseWeapon: BaseWeapon = new BaseWeapon();
}
