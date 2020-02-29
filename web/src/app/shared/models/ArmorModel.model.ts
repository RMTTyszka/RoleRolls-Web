import { Equipable } from './Equipable.model';
import { BaseArmor } from './BaseArmor.model';

export class ArmorModel extends Equipable {
    public baseArmor: BaseArmor = new BaseArmor();
}
