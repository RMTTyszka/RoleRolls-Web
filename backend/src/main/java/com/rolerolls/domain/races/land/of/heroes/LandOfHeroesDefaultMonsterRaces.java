package com.rolerolls.domain.races.land.of.heroes;

import com.rolerolls.domain.creatures.Attributes;
import com.rolerolls.domain.races.DefaultRace;
import com.rolerolls.shared.Bonus;
import com.rolerolls.shared.BonusType;

public class LandOfHeroesDefaultMonsterRaces {
    public static DefaultRace[] races = new DefaultRace[] {
            new DefaultRace("Undead", new Bonus[]{
                    new Bonus(Attributes.Vitality, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Agility, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Construct", new Bonus[]{
                    new Bonus(Attributes.Vitality, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Agility, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Elemental", new Bonus[]{
                    new Bonus(Attributes.Intuition, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Vitality, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Fey", new Bonus[]{
                    new Bonus(Attributes.Charisma, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Strength, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Animal", new Bonus[]{
                    new Bonus(Attributes.Intuition, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Agility, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Charisma, -2, 0, BonusType.Innate),
            }),
            new DefaultRace("Beast", new Bonus[]{
                    new Bonus(Attributes.Strength, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Vitality, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Wisdom, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Fiend", new Bonus[]{
                    new Bonus(Attributes.Charisma, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Wisdom, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Vitality, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Giant", new Bonus[]{
                    new Bonus(Attributes.Strength, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Agility, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Ooze", new Bonus[]{
                    new Bonus(Attributes.Vitality, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Agility, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Plant", new Bonus[]{
                    new Bonus(Attributes.Intuition, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Vitality, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Agility, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Celestial", new Bonus[]{
                    new Bonus(Attributes.Intuition, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Charisma, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Strength, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Dragon", new Bonus[]{
                    new Bonus(Attributes.Wisdom, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Agility, -2, 0, BonusType.Innate)
            }),
    };
}
