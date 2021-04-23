package com.rolerolls.domain.items.equipables.weapons.categories;

import java.util.Arrays;
import java.util.List;

public enum WeaponCategory {

    None, Light, Medium, Heavy, LightShield, MediumShield, HeavyShield;

    public static List<WeaponCategory> getList() {
        return Arrays.asList(None, Light, Medium, Heavy, LightShield, MediumShield, HeavyShield);
    }
}
