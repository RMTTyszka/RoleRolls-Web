import { Entity } from '../models/Entity.model';
import { ArmorInstance } from '../models/ArmorInstance.model';
import { ItemInstance } from '../models/ItemInstance.model';
import { WeaponInstance } from '../models/WeaponInstance.model';
import {GloveInstance} from '../models/GloveInstance.model';

export const isArmor = (armor: ItemInstance) => (armor as ArmorInstance).armorModel;
export const isWeapon = (armor: ItemInstance) => (armor as WeaponInstance).weaponModel;
export const isGlove = (item: ItemInstance) => (item as GloveInstance).gloveModel;
