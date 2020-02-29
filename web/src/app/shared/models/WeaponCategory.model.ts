import { AttributeEnum } from './Attributes.model';
import { Entity } from './Entity.model';

export class WeaponCategory extends Entity {
    public weaponType: WeaponType;
    public weaponHandleType: WeaponHandleType;
    public hitAttribute = AttributeEnum.strength;
    public damageAttribute = AttributeEnum.strength;
    public damageAttributeModifier = 1;
    public damageMagicBonusModifier = 1;

}

export enum WeaponType {
  light, medium, heavy
}
export enum WeaponHandleType {
  oneHanded,
  TwoHanbed
}
