package com.loh.domain.roles;

import com.loh.shared.Bonus;

public class DefaultRole {
    public String name;
    public Bonus[] bonuses;

    public DefaultRole(String name, Bonus[] bonuses) {
        this.name = name;
        this.bonuses = bonuses;
    }
}
