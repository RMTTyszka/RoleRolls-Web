import {EquipableSlot} from '@app/models/itens/equipable-slot';

export class ItemTemplate{
  public id: string;
  public campaignId: string;
  public powerId: string;
  public name: string;
  public type: ItemType;
}
export class EquipableTemplate extends ItemTemplate {
  public slot: EquipableSlot;
}
export class WeaponTemplate extends EquipableTemplate{
  public category: WeaponCategory;
}

export class ArmorTemplate extends EquipableTemplate{
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
