package com.rolerolls.domain.creatures;

public class CreatureLevel {
    public static Integer calculateExpToNextLevel(Integer level) {
        if (level > 1) {
            return calculateExpToNextLevel(level - 1) +
                    5 * x(level);
        } else {
            return 500;
        }
    }
    public static Integer x (Integer level) {
        if (level > 1) {
            return x(level - 1) + 50 * (level - 1);
        } else {
            return 100;
        }
    }
}
