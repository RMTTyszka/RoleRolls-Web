import {EquipableTemplate} from '../Equipable.model';
import {BaseArmor} from '../BaseArmor.model';

export class ArmorModel extends EquipableTemplate {
    public baseArmor: BaseArmor = new BaseArmor();
}
