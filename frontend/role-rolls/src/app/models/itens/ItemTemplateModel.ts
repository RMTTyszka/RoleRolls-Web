import { EquipableSlot } from 'app/models/itens/equipable-slot';

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
  Light = 0,
  Medium = 1,
  Heavy = 2,
}
export enum ArmorCategory
{
  Light = 0,
  Medium = 1,
  Heavy = 2,
}
