import { Entity } from '../models/Entity.model';
import { ArmorInstance } from '../models/ArmorInstance.model';
import { ItemInstance } from '../models/ItemInstance.model';
import { WeaponInstance } from '../models/WeaponInstance.model';
import {GloveInstance} from '../models/GloveInstance.model';
import {BeltInstance} from '../models/BeltInstance.model';
import {HeadpieceInstance} from '../models/HeadpieceInstance.model';
import {RingInstance} from '../models/RingInstance.model';

export const isArmor = (armor: ItemInstance) => (armor as ArmorInstance).armorModel;
export const isWeapon = (armor: ItemInstance) => (armor as WeaponInstance).weaponModel;
export const isGlove = (item: ItemInstance) => (item as GloveInstance).gloveModel;
export const isBelt = (item: ItemInstance) => (item as BeltInstance).beltModel;
export const isHeadpiece = (item: ItemInstance) => (item as HeadpieceInstance).headpieceModel;
export const isRing = (item: ItemInstance) => (item as RingInstance).ringModel;
