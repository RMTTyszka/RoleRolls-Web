import {WeaponCategory} from './WeaponCategory.model';
import {Entity} from './Entity.model';

export class BaseWeapon extends Entity {
    public category: WeaponCategory = WeaponCategory.None;
    public isStatic = false;
    public name = '';
}
