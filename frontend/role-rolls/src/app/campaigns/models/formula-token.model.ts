import { Property } from '@app/models/bonuses/bonus';
import { EquipableSlot } from '@app/models/itens/equipable-slot';

export enum FormulaTokenType {
  Property = 0,
  Creature = 3,
  Manual = 4,
  Equipment = 5,
}

export enum FormulaCreatureValue {
  ArmorDefenseBonus = 0,
  Level = 1,
  DefenseBonus1 = 2,
  DefenseBonus2 = 3,
  ArmorBonus = 4,
}

export enum FormulaEquipmentValue {
  LevelBonus = 0,
  DefenseBonus1 = 1,
  DefenseBonus2 = 2,
}

export interface FormulaToken {
  order: number;
  type: FormulaTokenType;
  property?: Property | null;
  operator?: string | null;
  value?: number | null;
  customValue?: FormulaCreatureValue | null;
  equipmentSlot?: EquipableSlot | null;
  equipmentValue?: FormulaEquipmentValue | null;
  manualValue?: string | null;
}
