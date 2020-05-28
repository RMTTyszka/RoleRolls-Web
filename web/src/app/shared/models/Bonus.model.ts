export class Bonus {
    id: string;
    property = '';
    level = 0;
    bonus = 0;
    BonusDurationEnum = BonusDurationEnum.Unending;
    remainingTurns = 1;
    bonusType: BonusTypeEnum = 'Innate';
    constructor(prop: string) {
      this.property = prop;
    }
}

export type BonusTypeEnum = 'Innate' | 'Magical' | 'Equipment';
    export const BonusTypeEnum = {
        Innate: 'Innate' as BonusTypeEnum,
        Magical: 'Magical' as BonusTypeEnum,
        Equipment: 'Equipment' as BonusTypeEnum
    };

export type BonusDurationEnum = 'Unending' | 'ByTurn';
    export const BonusDurationEnum = {
      Unending: 'Unending' as BonusDurationEnum,
      ByTurn: 'ByTurn' as BonusDurationEnum,
    };
