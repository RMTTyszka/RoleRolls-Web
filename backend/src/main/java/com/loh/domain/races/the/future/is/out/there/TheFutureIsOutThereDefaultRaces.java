package com.loh.domain.races.the.future.is.out.there;

import com.loh.domain.creatures.Attributes;
import com.loh.domain.races.DefaultRace;
import com.loh.shared.Bonus;
import com.loh.shared.BonusType;

public class TheFutureIsOutThereDefaultRaces {
    public static DefaultRace[] races = new DefaultRace[] {
            new DefaultRace("Human", new Bonus[]{}),
            new DefaultRace("Carrier Droid", new Bonus[]{
                    new Bonus(Attributes.Strength, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Agility, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Artesian Droid", new Bonus[]{
                    new Bonus(Attributes.Agility, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Wisdom, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Security Droid", new Bonus[]{
                    new Bonus(Attributes.Intuition, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Charisma, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Man-like Droid", new Bonus[]{
                    new Bonus(Attributes.Charisma, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Strength, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Data Droid", new Bonus[]{
                    new Bonus(Attributes.Wisdom, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Vitality, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Worker Droid", new Bonus[]{
                    new Bonus(Attributes.Vitality, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Intuition, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Mutant", new Bonus[]{
                    new Bonus(Attributes.Vitality, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Charisma, -2, 0, BonusType.Innate)
            }),
    };
}
