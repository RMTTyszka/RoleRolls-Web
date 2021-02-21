package com.loh.domain.items.equipables.armors;

public class DefaultEquipable {
    public String name;
    public String description;
    public String[] qualities;
    public String[] powers;

    public DefaultEquipable(String name, String description, String[] qualities, String[] powers) {
        this.name = name;
        this.description = description;
        this.qualities = qualities;
        this.powers = powers;
    }
}
