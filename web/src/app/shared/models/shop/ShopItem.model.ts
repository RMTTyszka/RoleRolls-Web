import {Entity} from '../Entity.model';
import {ArmorInstance} from '../items/ArmorInstance.model';
import {ItemInstance} from '../ItemInstance.model';
import {ItemTemplate} from '../Item.model';

export class ShopItem extends Entity {
  public quantity = 1;
  public quantityToBuy = 0;
  public value: number;
  public item: ItemTemplate;
  public get name() {
    return this.name;
  }
}
