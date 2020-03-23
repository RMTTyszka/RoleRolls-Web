import {BeltModel} from './BeltModel.model';
import {EquipableInstance} from './EquipableInstance.model';

export class BeltInstance extends EquipableInstance {
  public beltModel = new BeltModel();
}
