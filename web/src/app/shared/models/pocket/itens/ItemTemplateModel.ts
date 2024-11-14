import {EquipableSlotOld} from "src/app/shared/models/items/EquipableSlotOld";

export class ItemTemplateModel{
  public id: string;
  public campaignId: string;
  public powerId: string;
  public name: string;
  public type: ItemType;
}
export class EquipableTemplateModel extends ItemTemplateModel {
  public slot: EquipableSlotOld;
}
export class WeaponTemplateModel extends EquipableTemplateModel{
  public category: WeaponCategory;
}

export class ArmorTemplateModel extends EquipableTemplateModel{
  public category: ArmorCategory;
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
