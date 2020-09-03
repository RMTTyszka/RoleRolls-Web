import {Entity} from '../Entity.model';
import {ShopArmor} from './ShopArmor.model';
import {ShopItem} from './ShopItem.model';

export class Shop extends Entity {
    public items: Array<ShopItem>;
}
