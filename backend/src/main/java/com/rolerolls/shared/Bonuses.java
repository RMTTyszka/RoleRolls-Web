package com.rolerolls.shared;

import java.util.ArrayList;
import java.util.List;

public class Bonuses extends ArrayList<Bonus> implements IHaveBonuses {
    public static Integer GetEquipmentBonusLevel(List<Bonus> list,  String bonusName) {
        if (list != null) {
            Integer bonus = list.stream().filter(b -> b.getProperty().equals(bonusName) && b.getBonusType() == BonusType.Equipment)
                    .mapToInt(b -> b.getLevel())
                    .max().orElse(0);
            return bonus;
        }
        return 0;
    }

    public static Integer GetEquipmentBonus(List<Bonus> bonuses, String property) {
        if (bonuses != null) {
            Integer bonus = bonuses.stream().filter(b -> b.getProperty().equals(property) && b.getBonusType() == BonusType.Equipment)
                    .mapToInt(b -> b.getBonus())
                    .max().orElse(0);
            return bonus;
        }
        return 0;
    }
    public static Integer GetInnateBonusLevel(List<Bonus> list,  String bonusName) {
        if (list != null) {
            Integer bonus = list.stream().filter(b -> b.getProperty().equals(bonusName) && b.getBonusType() == BonusType.Innate)
                    .mapToInt(b -> b.getLevel())
                    .reduce(0, (a, b) -> a + b);
            return bonus;
        }
        return 0;
    }
    public static Integer GetMagicalBonusLevel(List<Bonus> list,  String bonusName) {
        if (list != null) {
            Integer bonus = list.stream().filter(b -> b.getProperty().equals(bonusName) && b.getBonusType() == BonusType.Magical)
                    .mapToInt(b -> b.getLevel())
                    .max().orElse(0);
            return bonus;
        }
        return 0;
    }

    public static Integer GetMagicalBonus(List<Bonus> bonuses, String property) {
        if (bonuses != null) {
            Integer bonus = bonuses.stream().filter(b -> b.getProperty().equals(property) && b.getBonusType() == BonusType.Magical)
                    .mapToInt(b -> b.getBonus())
                    .max().orElse(0);
            return bonus;
        }
        return 0;
    }
    public static Integer GetMoralBonusLevel(List<Bonus> list,  String bonusName) {
        if (list != null) {
            Integer bonus = list.stream().filter(b -> b.getProperty().equals(bonusName) && b.getBonusType() == BonusType.Moral)
                    .mapToInt(b -> b.getLevel())
                    .max().orElse(0);
            return bonus;
        }
        return 0;
    }

    public static Integer GetMoralBonus(List<Bonus> bonuses, String property) {
        if (bonuses != null) {
            Integer bonus = bonuses.stream().filter(b -> b.getProperty().equals(property) && b.getBonusType() == BonusType.Moral)
                    .mapToInt(b -> b.getBonus())
                    .max().orElse(0);
            return bonus;
        }
        return 0;
    }
    public static Integer GetBonus(List<Bonus> list,  String bonusName) {
        Bonus bonus = list.stream().filter(b -> b.getProperty().equals(bonusName)).findAny().orElse(null);
        return bonus != null ? bonus.getBonus() : 0;
    }

    public static Integer GetInnateBonus(List<Bonus> bonuses, String property) {
        if (bonuses != null) {
            Integer bonus = bonuses.stream().filter(b -> b.getProperty().equals(property) && b.getBonusType() == BonusType.Innate)
                    .mapToInt(b -> b.getBonus())
                    .reduce(0, (a, b) -> a + b);
            return bonus;
        }
        return 0;
    }

}

