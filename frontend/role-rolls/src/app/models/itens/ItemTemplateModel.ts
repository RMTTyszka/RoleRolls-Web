import { EquipableSlot } from 'app/models/itens/equipable-slot';

export type AnyItemTemplateModel = ItemTemplateModel | WeaponTemplateModel | ArmorTemplateModel;

export interface ItemTemplateModel{
  id: string;
  campaignId: string;
  powerId: string;
  name: string;
  type: ItemType;
}
export interface EquipableTemplateModel extends ItemTemplateModel {
  slot: EquipableSlot;
}
export interface WeaponTemplateModel extends EquipableTemplateModel{
  category: WeaponCategory;
  damageType: WeaponDamageType;
  isRanged: boolean;
  range: string | null;
}

export interface ArmorTemplateModel extends EquipableTemplateModel{
  category: ArmorCategory;
}
export enum ItemType {
  Consumable = 0,
  Weapon = 1,
  Armor = 2,
}
export enum WeaponCategory
{
  None = 0,
  Light = 1,
  Medium = 2,
  Heavy = 3,
  LightShield = 4,
  MediumShield = 5,
  HeavyShield = 6,
}
export enum ArmorCategory
{
  None = 0,
  Light = 1,
  Medium = 2,
  Heavy = 3,
}

export enum WeaponDamageType
{
  Cutting = 0,
  Bludgeoning = 1,
  Piercing = 2,
  Shield = 3,
}

export function isShieldCategory(category: WeaponCategory | null | undefined): boolean {
  return category === WeaponCategory.LightShield ||
    category === WeaponCategory.MediumShield ||
    category === WeaponCategory.HeavyShield;
}

export function weaponCategoryLabel(category: WeaponCategory | null | undefined): string {
  switch (category) {
    case WeaponCategory.Light:
      return 'Light';
    case WeaponCategory.Medium:
      return 'Medium';
    case WeaponCategory.Heavy:
      return 'Heavy';
    case WeaponCategory.LightShield:
      return 'Light Shield';
    case WeaponCategory.MediumShield:
      return 'Medium Shield';
    case WeaponCategory.HeavyShield:
      return 'Heavy Shield';
    default:
      return '';
  }
}

export function armorCategoryLabel(category: ArmorCategory | null | undefined): string {
  switch (category) {
    case ArmorCategory.Light:
      return 'Light';
    case ArmorCategory.Medium:
      return 'Medium';
    case ArmorCategory.Heavy:
      return 'Heavy';
    default:
      return '';
  }
}

export function weaponDamageTypeLabel(damageType: WeaponDamageType | null | undefined): string {
  switch (damageType) {
    case WeaponDamageType.Cutting:
      return 'Cutting';
    case WeaponDamageType.Bludgeoning:
      return 'Bludgeoning';
    case WeaponDamageType.Piercing:
      return 'Piercing';
    case WeaponDamageType.Shield:
      return 'Shield';
    default:
      return '';
  }
}
