import {Equipable, EquipableSlot} from './Equipable.model';
import {BaseArmor} from './BaseArmor.model';


export class ArmorModel extends Equipable {
  slot: EquipableSlot;
  baseArmor: BaseArmor = new BaseArmor();
}
