package com.loh.domain.roles.land.of.heroes;


import com.loh.domain.creatures.Attributes;
import com.loh.domain.roles.DefaultRole;
import com.loh.shared.Bonus;
import com.loh.shared.BonusType;

public class LohDefaultMonsterRoles {
    public static DefaultRole[] roles = new DefaultRole[]{
            new DefaultRole("Brute", new Bonus[]{
                    new Bonus(Attributes.Strength, 2, 0, BonusType.Innate)}),
            new DefaultRole("Battle Caster", new Bonus[]{
                    new Bonus(Attributes.Wisdom, 2, 0, BonusType.Innate)}),
            new DefaultRole("Assassin", new Bonus[]{
                    new Bonus(Attributes.Agility, 2, 0, BonusType.Innate)}),
            new DefaultRole("Shaman", new Bonus[]{
                    new Bonus(Attributes.Intuition, 2, 0, BonusType.Innate)}),
            new DefaultRole("Slanderer", new Bonus[]{
                    new Bonus(Attributes.Charisma, 2, 0, BonusType.Innate)}),
            new DefaultRole("Fighter", new Bonus[]{
                    new Bonus(Attributes.Vitality, 2, 0, BonusType.Innate)}),
    };
}
