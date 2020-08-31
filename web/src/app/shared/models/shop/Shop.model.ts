import {Entity} from '../Entity.model';
import {ShopArmor} from './ShopArmor.model';

export class Shop extends Entity {
    public armors: Array<ShopArmor>;
}
