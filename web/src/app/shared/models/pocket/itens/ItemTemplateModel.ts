export class ItemTemplateModel{
  public id: string;
  public campaignId: string;
  public powerId: string;
  public name: string;
  public type: ItemType;
}
export class WeaponTemplateModel extends ItemTemplateModel{
  public size: WeaponSize;
}
export enum ItemType {
  Consumable = 0,
  Equipable = 1
}
export enum WeaponSize
{
  Light = 0,
  Medium = 1,
  Heavy = 2,
}
