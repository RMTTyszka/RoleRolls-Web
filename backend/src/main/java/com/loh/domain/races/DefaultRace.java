package com.loh.domain.races;

import com.loh.shared.Bonus;

public class DefaultRace {
    public String name;
    public Bonus[] bonuses;

    public DefaultRace(String name, Bonus[] bonuses) {
        this.name = name;
        this.bonuses = bonuses;
    }
}
