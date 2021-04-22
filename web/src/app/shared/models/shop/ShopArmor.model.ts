import {Entity} from '../Entity.model';
import {ArmorInstance} from '../items/ArmorInstance.model';
import {ShopItem} from './ShopItem.model';

export class ShopArmor extends ShopItem {
    public armor: ArmorInstance;
    constructor() {
      super();
      this.name = this.armor.name;
    }
}
