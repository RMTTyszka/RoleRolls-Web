import {Entity} from '../Entity.model';
import {ItemTemplate} from '../items/ItemTemplate';

export class ShopItem extends Entity {
  public quantity = 1;
  public quantityToBuy = 0;
  public value: number;
  public item: ItemTemplate;
  public get name() {
    return this.name;
  }
}
