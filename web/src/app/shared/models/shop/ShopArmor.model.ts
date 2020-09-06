import {Entity} from '../Entity.model';
import {ArmorInstance} from '../items/ArmorInstance.model';
import {ShopItem} from './ShopItem.model';

export class ShopArmor extends ShopItem {
    public armor: ArmorInstance;
    public get name() {
      return this.armor.name;
    }
}
