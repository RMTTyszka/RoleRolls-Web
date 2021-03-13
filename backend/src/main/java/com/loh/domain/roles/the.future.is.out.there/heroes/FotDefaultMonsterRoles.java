package com.loh.domain.roles.the.future.is.out.there.heroes;


import com.loh.domain.creatures.Attributes;
import com.loh.domain.roles.DefaultRole;
import com.loh.shared.Bonus;
import com.loh.shared.BonusType;

public class FotDefaultMonsterRoles {
    public static DefaultRole[] roles = new DefaultRole[]{
            new DefaultRole("Fighter", new Bonus[]{
                    new Bonus(Attributes.Strength, 2, 0, BonusType.Innate)}),
            new DefaultRole("Scientist", new Bonus[]{
                    new Bonus(Attributes.Wisdom, 2, 0, BonusType.Innate)}),
            new DefaultRole("Specialist", new Bonus[]{
                    new Bonus(Attributes.Agility, 2, 0, BonusType.Innate)}),
            new DefaultRole("Skirmisher", new Bonus[]{
                    new Bonus(Attributes.Agility, 2, 0, BonusType.Innate)}),
            new DefaultRole("Researcher", new Bonus[]{
                    new Bonus(Attributes.Intuition, 2, 0, BonusType.Innate)}),
            new DefaultRole("Influencer", new Bonus[]{
                    new Bonus(Attributes.Charisma, 2, 0, BonusType.Innate)}),
            new DefaultRole("Brawler", new Bonus[]{
                    new Bonus(Attributes.Vitality, 2, 0, BonusType.Innate)}),
    };
}
