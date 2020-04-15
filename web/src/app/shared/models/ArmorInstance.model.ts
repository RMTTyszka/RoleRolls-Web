import {EquipableInstance} from './EquipableInstance.model';
import {ArmorModel} from './ArmorModel.model';

export class ArmorInstance extends EquipableInstance {

  public armorModel: ArmorModel;
  public isArmor = true;
}
