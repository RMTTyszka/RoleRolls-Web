import { AttributeEnum } from './Attributes.model';
import { Entity } from './Entity.model';

export class WeaponCategory extends Entity {
    public weaponType: WeaponType;
    public weaponHandleType: WeaponHandleType;
}

export enum WeaponType {
  light, medium, heavy, Shield, None
}
export enum WeaponHandleType {
  oneHanded,
  TwoHanbed
}
