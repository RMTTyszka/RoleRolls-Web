package com.loh.shared;

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
    public static Integer GetMoralBonusLevel(List<Bonus> list,  String bonusName) {
        if (list != null) {
            Integer bonus = list.stream().filter(b -> b.getProperty().equals(bonusName) && b.getBonusType() == BonusType.Moral)
                    .mapToInt(b -> b.getLevel())
                    .max().orElse(0);
            return bonus;
        }
        return 0;
    }
    public static Integer GetBonus(List<Bonus> list,  String bonusName) {
        Bonus bonus = list.stream().filter(b -> b.getProperty().equals(bonusName)).findAny().orElse(null);
        return bonus != null ? bonus.getBonus() : 0;
    }
}

