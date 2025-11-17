import { Property } from '@app/models/bonuses/bonus';

export enum FormulaTokenType {
  Property = 0,
  Number = 1,
  Operator = 2,
  CustomValue = 3
}

export enum FormulaCustomValue {
  ArmorDefenseBonus = 0,
  Level = 1
}

export interface FormulaToken {
  order: number;
  type: FormulaTokenType;
  property?: Property | null;
  operator?: string | null;
  value?: number | null;
  customValue?: FormulaCustomValue | null;
}
