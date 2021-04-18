import {BaseWeapon} from './BaseWeapon.model';
import {EquipableTemplate} from './items/EquipableTemplate';

export class WeaponModel extends EquipableTemplate {
  baseWeapon: BaseWeapon = new BaseWeapon();
}
