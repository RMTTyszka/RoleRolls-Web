package com.rolerolls.domain.items.equipables.armors.categories;

import lombok.Getter;

import java.util.Arrays;
import java.util.List;

public enum ArmorCategory  {

    None, Light, Medium, Heavy;

    @Getter
    private int defense;
    @Getter
    private int dodge;
    @Getter
    private int evasion;
    @Getter
    private int baseDefense;

    static {
        None.defense = 0;
        Light.defense = 1;
        Medium.defense = 2;
        Heavy.defense = 3;

        None.baseDefense = 0;
        Light.baseDefense = 2;
        Medium.baseDefense = 3;
        Heavy.baseDefense = 4;

        None.dodge = 1;
        Light.dodge = 1;
        Medium.dodge = 0;
        Heavy.dodge = -1;

        None.evasion = 1;
        Light.evasion = 1;
        Medium.evasion = 0;
        Heavy.evasion = -1;
    }
    public static List<ArmorCategory> getList() {
        return Arrays.asList(None, Light, Medium, Heavy);
    }

}

