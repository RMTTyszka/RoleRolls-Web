package com.loh.creatures.heroes.equipment;

import lombok.Getter;

public enum GripType {
    OneLightWeapon,
    OneMediumWeapon,
    TwoHandedHeavyWeapon,
    TwoWeaponsLight,
    TwoWeaponsMedium,
    OneHandedHeavyWeapon,
    TwoHandedMediumWeapon;

    @Getter
    private Integer hit;
    @Getter
    private Integer damage;
    @Getter
    private Integer magicBonusModifier;
    @Getter
    private Integer attributeModifier;
    @Getter
    private Integer attackComplexity;

    static {
        OneLightWeapon.hit = -1;
        OneMediumWeapon.hit = 2;
        TwoHandedHeavyWeapon.hit = 3;
        TwoWeaponsLight.hit = -2;
        TwoWeaponsMedium.hit = 0;
        OneHandedHeavyWeapon.hit = -1;
        TwoHandedMediumWeapon.hit = 2;

        OneLightWeapon.damage = 4;
        OneMediumWeapon.damage = 10;
        TwoHandedHeavyWeapon.damage = 12;
        TwoWeaponsLight.damage = 4;
        TwoWeaponsMedium.damage = 8;
        OneHandedHeavyWeapon.damage = 10;
        TwoHandedMediumWeapon.damage = 10;

        OneLightWeapon.magicBonusModifier = 3;
        OneMediumWeapon.magicBonusModifier = 3;
        TwoHandedHeavyWeapon.magicBonusModifier = 4;
        TwoWeaponsLight.magicBonusModifier = 2;
        TwoWeaponsMedium.magicBonusModifier = 3;
        OneHandedHeavyWeapon.magicBonusModifier = 4;
        TwoHandedMediumWeapon.magicBonusModifier = 4;

        OneLightWeapon.attributeModifier = 2;
        OneMediumWeapon.attributeModifier = 3;
        TwoHandedHeavyWeapon.attributeModifier = 4;
        TwoWeaponsLight.attributeModifier = 2;
        TwoWeaponsMedium.attributeModifier = 2;
        OneHandedHeavyWeapon.attributeModifier = 3;
        TwoHandedMediumWeapon.attributeModifier = 4;

        OneLightWeapon.attackComplexity = 1;
        OneMediumWeapon.attackComplexity = 2;
        TwoHandedHeavyWeapon.attackComplexity = 3;
        TwoWeaponsLight.attackComplexity = 1;
        TwoWeaponsMedium.attackComplexity = 2;
        OneHandedHeavyWeapon.attackComplexity = 2;
        TwoHandedMediumWeapon.attackComplexity = 3;
    }
}
