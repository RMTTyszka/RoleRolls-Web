import {Entity} from './Entity.model';
import {ArmorModel} from './Armor.model';
import {ItemInstance} from './ItemInstance.model';

export class ArmorInstance extends ItemInstance {

  public armorModel: ArmorModel;
  public isArmor = true;
}
