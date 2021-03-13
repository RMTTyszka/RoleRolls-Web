package com.loh.domain.roles.the.future.is.out.there.heroes;


import com.loh.domain.creatures.Attributes;
import com.loh.domain.roles.DefaultRole;
import com.loh.shared.Bonus;
import com.loh.shared.BonusType;

public class FotDefaultHeroRoles {
    public static DefaultRole[] roles = new DefaultRole[]{
            new DefaultRole("Infantry", new Bonus[]{
                    new Bonus(Attributes.Strength, 2, 0, BonusType.Innate)}),
            new DefaultRole("Guerrilla", new Bonus[]{
                    new Bonus(Attributes.Strength, 2, 0, BonusType.Innate)}),
            new DefaultRole("Engineer", new Bonus[]{
                    new Bonus(Attributes.Wisdom, 2, 0, BonusType.Innate)}),
            new DefaultRole("Hacker", new Bonus[]{
                    new Bonus(Attributes.Wisdom, 2, 0, BonusType.Innate)}),
            new DefaultRole("Thief", new Bonus[]{
                    new Bonus(Attributes.Agility, 2, 0, BonusType.Innate)}),
            new DefaultRole("Sharpshooter", new Bonus[]{
                    new Bonus(Attributes.Agility, 2, 0, BonusType.Innate)}),
            new DefaultRole("Sociologist", new Bonus[]{
                    new Bonus(Attributes.Intuition, 2, 0, BonusType.Innate)}),
            new DefaultRole("Biologist", new Bonus[]{
                    new Bonus(Attributes.Intuition, 2, 0, BonusType.Innate)}),
            new DefaultRole("Pilot", new Bonus[]{
                    new Bonus(Attributes.Intuition, 2, 0, BonusType.Innate)}),
            new DefaultRole("Influencer", new Bonus[]{
                    new Bonus(Attributes.Charisma, 2, 0, BonusType.Innate)}),
            new DefaultRole("Leader", new Bonus[]{
                    new Bonus(Attributes.Charisma, 2, 0, BonusType.Innate)}),
            new DefaultRole("Prototype", new Bonus[]{
                    new Bonus(Attributes.Vitality, 2, 0, BonusType.Innate)}),
            new DefaultRole("Brawler", new Bonus[]{
                    new Bonus(Attributes.Vitality, 2, 0, BonusType.Innate)}),
    };
}
