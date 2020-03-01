import { WeaponCategory } from './WeaponCategory.model';
import { Entity } from './Entity.model';

export class BaseWeapon extends Entity {
    public weaponCategory: WeaponCategory = new WeaponCategory();
    public isStatic = true;
    public name = '';
}
