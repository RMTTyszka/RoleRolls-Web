import {ItemInstance} from './ItemInstance.model';
import {Entity} from './Entity.model';

export class Inventory extends Entity {
  public items: ItemInstance[];
  public cash1: number;
  public cash2: number;
  public cash3: number;
}
