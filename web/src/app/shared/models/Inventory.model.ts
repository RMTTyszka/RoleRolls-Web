import {ItemInstance} from './ItemInstance.model';
import { Entity } from './Entity.model';

export class Inventory extends Entity {
  public items: ItemInstance[];
}
