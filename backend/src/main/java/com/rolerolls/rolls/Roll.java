package com.rolerolls.rolls;

public class Roll {
    public Integer dice;
    public Integer bonus;
    public Integer bonusDice;
    public boolean success;
    public boolean criticalSuccess;
    public boolean criticalFailure;
    public Roll() {
    }

    public Roll(Integer dice, Integer bonus, boolean success) {
        this(dice, bonus, success, null);
    }
    public Roll(Integer dice, Integer bonus, boolean success, Integer bonusDice) {
        this.dice = dice;
        this.bonus = bonus;
        this.success = success;
        this.bonusDice = bonusDice;
        this.criticalFailure = dice == 1;
        this.criticalSuccess = dice == 20;
    }
}
