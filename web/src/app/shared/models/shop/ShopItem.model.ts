import {Entity} from '../Entity.model';
import {ArmorInstance} from '../ArmorInstance.model';
import {ItemInstance} from '../ItemInstance.model';

export class ShopItem extends Entity {
  public quantity = 1;
  public quantityToBuy = 0;
  public value: number;
  public item: ItemInstance;
  public get name() {
    return this.name;
  }
}
