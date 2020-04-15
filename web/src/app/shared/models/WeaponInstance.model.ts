import {EquipableInstance} from './EquipableInstance.model';
import {WeaponModel} from './WeaponModel.model';

export class WeaponInstance extends EquipableInstance {

  public weaponModel: WeaponModel;
  public isWeapon = true;
}
