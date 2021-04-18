import {BaseArmor} from '../BaseArmor.model';
import {EquipableTemplate} from './EquipableTemplate';

export class ArmorModel extends EquipableTemplate {
    public baseArmor: BaseArmor = new BaseArmor();
}
