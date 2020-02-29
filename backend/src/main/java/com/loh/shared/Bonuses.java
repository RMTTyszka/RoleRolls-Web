package com.loh.shared;

import java.util.ArrayList;
import java.util.List;

public class Bonuses extends ArrayList<Bonus> implements IHaveBonuses {
    public static Integer GetBonusLevel(List<Bonus> list,  String bonusName) {
        Bonus bonus = list.stream().filter(b -> bonusName.equals(bonusName)).findAny().orElse(null);
        return bonus != null ? bonus.getLevel() : 0;
    }
    public static Integer GetBonus(List<Bonus> list,  String bonusName) {
        Bonus bonus = list.stream().filter(b -> bonusName.equals(bonusName)).findAny().orElse(null);
        return bonus != null ? bonus.getBonus() : 0;
    }
}

