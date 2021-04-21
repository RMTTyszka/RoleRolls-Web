import {EquipableInstance} from '../EquipableInstance.model';
import {ArmorModel} from './ArmorModel.model';

export class ArmorInstance extends EquipableInstance {

  public armorTemplate: ArmorModel;
  public isArmor = true;
}
