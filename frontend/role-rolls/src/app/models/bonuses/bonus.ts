export interface Bonus {
  id: string;
  value: number;
  valueType: BonusValueType;
  property: Property;
  type: BonusType;
  name: string;
  description: string;
}

enum BonusValueType {
  Dices = 0,
  Roll = 1
}

export interface Property {
}

enum BonusType {
  Innate = 0
}
