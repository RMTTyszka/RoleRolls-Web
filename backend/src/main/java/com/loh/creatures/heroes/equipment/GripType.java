package com.loh.creatures.heroes.equipment;

import lombok.Getter;

public enum GripType {
    OneLightWeapon,
    OneMediumWeapon,
    TwoHandedHeavyWeapon,
    TwoWeaponsLight,
    TwoWeaponsMedium,
    OneHandedHeavyWeapon,
    TwoHandedMediumWeapon,
    OneLightShield,
    OneMediumShield,
    OneHeavyShield;

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
    @Getter
    private Integer shieldHitBonus;
    @Getter
    private Integer shieldEvasionBonus;

    static {
        OneLightWeapon.hit = -3;
        OneMediumWeapon.hit = 1;
        TwoHandedHeavyWeapon.hit = 3;
        TwoWeaponsLight.hit = 0;
        TwoWeaponsMedium.hit = 1;
        OneHandedHeavyWeapon.hit = -1;
        TwoHandedMediumWeapon.hit = 2;
        OneLightShield.hit = 0;
        OneMediumShield.hit = 1;
        OneHeavyShield.hit = 3;

        OneLightWeapon.damage = 4;
        OneMediumWeapon.damage = 10;
        TwoHandedHeavyWeapon.damage = 12;
        TwoWeaponsLight.damage = 4;
        TwoWeaponsMedium.damage = 8;
        OneHandedHeavyWeapon.damage = 10;
        TwoHandedMediumWeapon.damage = 10;
        OneLightShield.damage = 4;
        OneMediumShield.damage = 8;
        OneHeavyShield.damage = 12;

        OneLightWeapon.magicBonusModifier = 3;
        OneMediumWeapon.magicBonusModifier = 3;
        TwoHandedHeavyWeapon.magicBonusModifier = 4;
        TwoWeaponsLight.magicBonusModifier = 2;
        TwoWeaponsMedium.magicBonusModifier = 3;
        OneHandedHeavyWeapon.magicBonusModifier = 4;
        TwoHandedMediumWeapon.magicBonusModifier = 4;
        OneLightShield.magicBonusModifier = 2;
        OneMediumShield.magicBonusModifier = 3;
        OneHeavyShield.magicBonusModifier = 4;

        OneLightWeapon.attributeModifier = 2;
        OneMediumWeapon.attributeModifier = 3;
        TwoHandedHeavyWeapon.attributeModifier = 4;
        TwoWeaponsLight.attributeModifier = 2;
        TwoWeaponsMedium.attributeModifier = 2;
        OneHandedHeavyWeapon.attributeModifier = 3;
        TwoHandedMediumWeapon.attributeModifier = 4;
        OneLightShield.attributeModifier = 2;
        OneMediumShield.attributeModifier = 2;
        OneHeavyShield.attributeModifier = 4;

        OneLightWeapon.attackComplexity = 1;
        OneMediumWeapon.attackComplexity = 2;
        TwoHandedHeavyWeapon.attackComplexity = 3;
        TwoWeaponsLight.attackComplexity = 1;
        TwoWeaponsMedium.attackComplexity = 2;
        OneHandedHeavyWeapon.attackComplexity = 2;
        TwoHandedMediumWeapon.attackComplexity = 3;
        OneLightShield.attackComplexity = 1;
        OneMediumShield.attackComplexity = 2;
        OneHeavyShield.attackComplexity = 3;

        OneLightWeapon.shieldEvasionBonus = 0;
        OneMediumWeapon.shieldEvasionBonus = 0;
        TwoHandedHeavyWeapon.shieldEvasionBonus = 0;
        TwoWeaponsLight.shieldEvasionBonus = 0;
        TwoWeaponsMedium.shieldEvasionBonus = 0;
        OneHandedHeavyWeapon.shieldEvasionBonus = 0;
        TwoHandedMediumWeapon.shieldEvasionBonus = 0;
        OneLightShield.shieldEvasionBonus = 0;
        OneMediumShield.shieldEvasionBonus = 0;
        OneHeavyShield.shieldEvasionBonus = 0;

        OneLightWeapon.shieldHitBonus = 0;
        OneMediumWeapon.shieldHitBonus = 0;
        TwoHandedHeavyWeapon.shieldHitBonus = 0;
        TwoWeaponsLight.shieldHitBonus = 0;
        TwoWeaponsMedium.shieldHitBonus = 0;
        OneHandedHeavyWeapon.shieldHitBonus = 0;
        TwoHandedMediumWeapon.shieldHitBonus = 0;
        OneLightShield.shieldHitBonus = 0;
        OneMediumShield.shieldHitBonus = -1;
        OneHeavyShield.shieldHitBonus = -2;
    }
}
