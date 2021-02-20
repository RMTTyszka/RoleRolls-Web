package com.loh.domain.items.equipables.armors;

public class DefaultArmor {
    public String name;

    public DefaultArmor(String name, String description, String[] qualities, String[] powers) {
        this.name = name;
        this.description = description;
        this.qualities = qualities;
        this.powers = powers;
    }

    public String description;
    public String[] qualities;
    public String[] powers;
}
