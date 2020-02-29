export class Bonus {
    property = '';
    level = 0;
    bonus = 0;
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
