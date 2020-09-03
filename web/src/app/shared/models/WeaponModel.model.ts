import {EquipableTemplate} from './Equipable.model';
import {BaseWeapon} from './BaseWeapon.model';

export class WeaponModel extends EquipableTemplate {
  baseWeapon: BaseWeapon = new BaseWeapon();
}
