package com.loh.items.equipable.weapons.weaponCategory;

import lombok.Getter;

import java.util.Arrays;
import java.util.List;

public enum WeaponCategory {

    None, Light, Medium, Heavy, LightShield, MediumShield, HeavyShield;

    @Getter
    private WeaponHandleType handleType;

    public static List<WeaponCategory> getList() {
        return Arrays.asList(None, Light, Medium, Heavy, LightShield, MediumShield, HeavyShield);
    }
}
