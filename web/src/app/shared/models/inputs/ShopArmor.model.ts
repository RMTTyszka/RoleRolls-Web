import {Entity} from '../Entity.model';
import {ArmorInstance} from '../ArmorInstance.model';

export class ShopArmor extends Entity {
    public armor: ArmorInstance;
    public quantity: number;
    public value: number;
}
