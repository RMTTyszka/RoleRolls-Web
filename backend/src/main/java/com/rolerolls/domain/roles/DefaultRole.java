package com.rolerolls.domain.roles;

import com.rolerolls.shared.Bonus;

public class DefaultRole {
    public String name;
    public Bonus[] bonuses;

    public DefaultRole(String name, Bonus[] bonuses) {
        this.name = name;
        this.bonuses = bonuses;
    }
}
