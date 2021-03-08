package com.loh.domain.races.land.of.heroes;

import com.loh.domain.creatures.Attributes;
import com.loh.domain.races.DefaultRace;
import com.loh.shared.Bonus;
import com.loh.shared.BonusType;

public class LandOfHeroesDefaultHeroRaces {
    public static DefaultRace[] races = new DefaultRace[] {
            new DefaultRace("Human", new Bonus[]{}),
            new DefaultRace("Elf", new Bonus[]{
                    new Bonus(Attributes.Agility, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Vitality, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Dwarf", new Bonus[]{
                    new Bonus(Attributes.Vitality, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Charisma, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Orc", new Bonus[]{
                    new Bonus(Attributes.Strength, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Wisdom, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Gnome", new Bonus[]{
                    new Bonus(Attributes.Wisdom, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Strength, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Draekar", new Bonus[]{
                    new Bonus(Attributes.Strength, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Vitality, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Intuition, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Daennan", new Bonus[]{
                    new Bonus(Attributes.Wisdom, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Intuition, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Vitality, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Rhassin", new Bonus[]{
                    new Bonus(Attributes.Agility, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Vitality, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Wisdom, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Goblin", new Bonus[]{
                    new Bonus(Attributes.Wisdom, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Charisma, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Strength, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Halfling", new Bonus[]{
                    new Bonus(Attributes.Agility, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Vitality, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Strength, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Ku-Toa", new Bonus[]{
                    new Bonus(Attributes.Wisdom, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Strength, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Agility, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Trolling", new Bonus[]{
                    new Bonus(Attributes.Intuition, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Vitality, 2, 0, BonusType.Innate),
                    new Bonus(Attributes.Wisdom, -2, 0, BonusType.Innate)
            }),
            new DefaultRace("Tharian", new Bonus[]{
                    new Bonus(Attributes.Intuition, 4, 0, BonusType.Innate),
                    new Bonus(Attributes.Vitality, -2, 0, BonusType.Innate)
            }),
    };
}
