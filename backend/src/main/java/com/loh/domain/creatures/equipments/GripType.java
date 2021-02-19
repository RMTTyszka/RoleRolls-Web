package com.loh.domain.creatures.equipments;

import com.loh.domain.items.equipables.weapons.categories.WeaponCategory;
import lombok.Getter;

import java.util.HashMap;
import java.util.Map;

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
    OneHeavyShield,
    None;

    @Getter
    private Integer hit;
    @Getter
    private Integer damage;
    @Getter
    private Integer baseBonusDamage;
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

    public static Map<GripType, Map<GripType, GripType>> MainGripType;

    static {
        OneLightWeapon.hit = 1;
        OneMediumWeapon.hit = 2;
        TwoHandedHeavyWeapon.hit = 2;
        TwoWeaponsLight.hit = 0;
        TwoWeaponsMedium.hit = 1;
        OneHandedHeavyWeapon.hit = 0;
        TwoHandedMediumWeapon.hit = 2;
        OneLightShield.hit = 0;
        OneMediumShield.hit = 1;
        OneHeavyShield.hit = 3;
        None.hit = 0;

        OneLightWeapon.damage = 6;
        OneMediumWeapon.damage = 10;
        TwoHandedHeavyWeapon.damage = 12;
        TwoWeaponsLight.damage = 4;
        TwoWeaponsMedium.damage = 8;
        OneHandedHeavyWeapon.damage = 10;
        TwoHandedMediumWeapon.damage = 10;
        OneLightShield.damage = 4;
        OneMediumShield.damage = 8;
        OneHeavyShield.damage = 12;
        None.damage = 0;

        OneLightWeapon.baseBonusDamage = 2;
        OneMediumWeapon.baseBonusDamage = 0;
        TwoHandedHeavyWeapon.baseBonusDamage = 0;
        TwoWeaponsLight.baseBonusDamage = 0;
        TwoWeaponsMedium.baseBonusDamage = 2;
        OneHandedHeavyWeapon.baseBonusDamage = 0;
        TwoHandedMediumWeapon.baseBonusDamage = 0;
        OneLightShield.baseBonusDamage = 0;
        OneMediumShield.baseBonusDamage = 0;
        OneHeavyShield.baseBonusDamage = 0;
        None.baseBonusDamage = 0;

        OneLightWeapon.magicBonusModifier = 2;
        OneMediumWeapon.magicBonusModifier = 3;
        TwoHandedHeavyWeapon.magicBonusModifier = 4;
        TwoWeaponsLight.magicBonusModifier = 2;
        TwoWeaponsMedium.magicBonusModifier = 3;
        OneHandedHeavyWeapon.magicBonusModifier = 4;
        TwoHandedMediumWeapon.magicBonusModifier = 4;
        OneLightShield.magicBonusModifier = 2;
        OneMediumShield.magicBonusModifier = 3;
        OneHeavyShield.magicBonusModifier = 4;
        None.magicBonusModifier = 0;

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
        None.attributeModifier = 0;

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
        None.attackComplexity = 0;

        OneLightWeapon.shieldEvasionBonus = 0;
        OneMediumWeapon.shieldEvasionBonus = 0;
        TwoHandedHeavyWeapon.shieldEvasionBonus = 0;
        TwoWeaponsLight.shieldEvasionBonus = 0;
        TwoWeaponsMedium.shieldEvasionBonus = 0;
        OneHandedHeavyWeapon.shieldEvasionBonus = 0;
        TwoHandedMediumWeapon.shieldEvasionBonus = 0;
        OneLightShield.shieldEvasionBonus = 1;
        OneMediumShield.shieldEvasionBonus = 2;
        OneHeavyShield.shieldEvasionBonus = 3;
        None.shieldEvasionBonus = 0;

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
        None.shieldHitBonus = 0;

        MainGripType = new HashMap<>();
        for (GripType gripType1 : GripType.values()) {
            MainGripType.put(gripType1, new HashMap<>());
        }
        MainGripType.get(GripType.OneLightWeapon).put(GripType.OneLightWeapon, GripType.TwoWeaponsLight);
        MainGripType.get(GripType.OneLightWeapon).put(GripType.OneMediumWeapon, GripType.TwoWeaponsLight);
        MainGripType.get(GripType.OneLightWeapon).put(GripType.TwoWeaponsLight, GripType.TwoWeaponsLight);
        MainGripType.get(GripType.OneLightWeapon).put(GripType.TwoWeaponsMedium, GripType.TwoWeaponsLight);
        MainGripType.get(GripType.OneLightWeapon).put(GripType.TwoHandedHeavyWeapon, null);
        MainGripType.get(GripType.OneLightWeapon).put(GripType.TwoHandedMediumWeapon, null);
        MainGripType.get(GripType.OneLightWeapon).put(GripType.OneHandedHeavyWeapon, null);
        MainGripType.get(GripType.OneLightWeapon).put(GripType.OneLightShield, GripType.OneLightWeapon);
        MainGripType.get(GripType.OneLightWeapon).put(GripType.OneMediumShield, GripType.OneLightWeapon);
        MainGripType.get(GripType.OneLightWeapon).put(GripType.OneHeavyShield, GripType.OneLightWeapon);
        MainGripType.get(GripType.OneLightWeapon).put(null, GripType.OneLightWeapon);
        MainGripType.get(GripType.OneLightWeapon).put(GripType.None, GripType.OneLightWeapon);

        MainGripType.get(GripType.OneMediumWeapon).put(GripType.OneLightWeapon, GripType.TwoWeaponsMedium);
        MainGripType.get(GripType.OneMediumWeapon).put(GripType.OneMediumWeapon, GripType.TwoWeaponsMedium);
        MainGripType.get(GripType.OneMediumWeapon).put(GripType.TwoWeaponsLight, GripType.TwoWeaponsMedium);
        MainGripType.get(GripType.OneMediumWeapon).put(GripType.TwoWeaponsMedium, GripType.TwoWeaponsMedium);
        MainGripType.get(GripType.OneMediumWeapon).put(GripType.TwoHandedHeavyWeapon, null);
        MainGripType.get(GripType.OneMediumWeapon).put(GripType.TwoHandedMediumWeapon, null);
        MainGripType.get(GripType.OneMediumWeapon).put(GripType.OneHandedHeavyWeapon, null);
        MainGripType.get(GripType.OneMediumWeapon).put(GripType.OneLightShield, GripType.OneMediumWeapon);
        MainGripType.get(GripType.OneMediumWeapon).put(GripType.OneMediumShield, GripType.OneMediumWeapon);
        MainGripType.get(GripType.OneMediumWeapon).put(GripType.OneHeavyShield, GripType.OneMediumWeapon);
        MainGripType.get(GripType.OneMediumWeapon).put(null, GripType.OneMediumWeapon);
        MainGripType.get(GripType.OneMediumWeapon).put(GripType.None, GripType.OneMediumWeapon);

        MainGripType.get(GripType.TwoHandedHeavyWeapon).put(GripType.OneLightWeapon, null);
        MainGripType.get(GripType.TwoHandedHeavyWeapon).put(GripType.OneMediumWeapon, null);
        MainGripType.get(GripType.TwoHandedHeavyWeapon).put(GripType.TwoWeaponsLight, null);
        MainGripType.get(GripType.TwoHandedHeavyWeapon).put(GripType.TwoWeaponsMedium, null);
        MainGripType.get(GripType.TwoHandedHeavyWeapon).put(GripType.TwoHandedHeavyWeapon, null);
        MainGripType.get(GripType.TwoHandedHeavyWeapon).put(GripType.TwoHandedMediumWeapon, null);
        MainGripType.get(GripType.TwoHandedHeavyWeapon).put(GripType.OneHandedHeavyWeapon, null);
        MainGripType.get(GripType.TwoHandedHeavyWeapon).put(GripType.OneLightShield, null);
        MainGripType.get(GripType.TwoHandedHeavyWeapon).put(GripType.OneMediumShield, null);
        MainGripType.get(GripType.TwoHandedHeavyWeapon).put(GripType.OneHeavyShield, null);
        MainGripType.get(GripType.TwoHandedHeavyWeapon).put(null, TwoHandedHeavyWeapon);
        MainGripType.get(GripType.TwoHandedHeavyWeapon).put(GripType.None, TwoHandedHeavyWeapon);

        MainGripType.get(GripType.TwoWeaponsLight).put(GripType.OneLightWeapon, TwoWeaponsLight);
        MainGripType.get(GripType.TwoWeaponsLight).put(GripType.OneMediumWeapon, TwoWeaponsLight);
        MainGripType.get(GripType.TwoWeaponsLight).put(GripType.TwoWeaponsLight, TwoWeaponsLight);
        MainGripType.get(GripType.TwoWeaponsLight).put(GripType.TwoWeaponsMedium, TwoWeaponsLight);
        MainGripType.get(GripType.TwoWeaponsLight).put(GripType.TwoHandedHeavyWeapon, null);
        MainGripType.get(GripType.TwoWeaponsLight).put(GripType.TwoHandedMediumWeapon, null);
        MainGripType.get(GripType.TwoWeaponsLight).put(GripType.OneHandedHeavyWeapon, null);
        MainGripType.get(GripType.TwoWeaponsLight).put(GripType.OneLightShield, null);
        MainGripType.get(GripType.TwoWeaponsLight).put(GripType.OneMediumShield, null);
        MainGripType.get(GripType.TwoWeaponsLight).put(GripType.OneHeavyShield, null);
        MainGripType.get(GripType.TwoWeaponsLight).put(null, OneLightWeapon);
        MainGripType.get(GripType.TwoWeaponsLight).put(GripType.None, OneLightWeapon);

        MainGripType.get(GripType.TwoWeaponsMedium).put(GripType.OneLightWeapon, TwoWeaponsMedium);
        MainGripType.get(GripType.TwoWeaponsMedium).put(GripType.OneMediumWeapon, TwoWeaponsMedium);
        MainGripType.get(GripType.TwoWeaponsMedium).put(GripType.TwoWeaponsLight, TwoWeaponsMedium);
        MainGripType.get(GripType.TwoWeaponsMedium).put(GripType.TwoWeaponsMedium, TwoWeaponsMedium);
        MainGripType.get(GripType.TwoWeaponsMedium).put(GripType.TwoHandedHeavyWeapon, null);
        MainGripType.get(GripType.TwoWeaponsMedium).put(GripType.TwoHandedMediumWeapon, null);
        MainGripType.get(GripType.TwoWeaponsMedium).put(GripType.OneHandedHeavyWeapon, null);
        MainGripType.get(GripType.TwoWeaponsMedium).put(GripType.OneLightShield, null);
        MainGripType.get(GripType.TwoWeaponsMedium).put(GripType.OneMediumShield, null);
        MainGripType.get(GripType.TwoWeaponsMedium).put(GripType.OneHeavyShield, null);
        MainGripType.get(GripType.TwoWeaponsMedium).put(null, OneMediumWeapon);
        MainGripType.get(GripType.TwoWeaponsMedium).put(GripType.None, OneMediumWeapon);

        MainGripType.get(GripType.OneHandedHeavyWeapon).put(GripType.OneLightWeapon, null);
        MainGripType.get(GripType.OneHandedHeavyWeapon).put(GripType.OneMediumWeapon, null);
        MainGripType.get(GripType.OneHandedHeavyWeapon).put(GripType.TwoWeaponsLight, null);
        MainGripType.get(GripType.OneHandedHeavyWeapon).put(GripType.TwoWeaponsMedium, null);
        MainGripType.get(GripType.OneHandedHeavyWeapon).put(GripType.TwoHandedHeavyWeapon, null);
        MainGripType.get(GripType.OneHandedHeavyWeapon).put(GripType.TwoHandedMediumWeapon, null);
        MainGripType.get(GripType.OneHandedHeavyWeapon).put(GripType.OneHandedHeavyWeapon, null);
        MainGripType.get(GripType.OneHandedHeavyWeapon).put(GripType.OneLightShield, GripType.OneHandedHeavyWeapon);
        MainGripType.get(GripType.OneHandedHeavyWeapon).put(GripType.OneMediumShield, GripType.OneHandedHeavyWeapon);
        MainGripType.get(GripType.OneHandedHeavyWeapon).put(GripType.OneHeavyShield, GripType.OneHandedHeavyWeapon);
        MainGripType.get(GripType.OneHandedHeavyWeapon).put(null, OneHandedHeavyWeapon);
        MainGripType.get(GripType.OneHandedHeavyWeapon).put(GripType.None, OneHandedHeavyWeapon);

        MainGripType.get(GripType.OneLightShield).put(GripType.OneLightWeapon, OneLightShield);
        MainGripType.get(GripType.OneLightShield).put(GripType.OneMediumWeapon, OneLightShield);
        MainGripType.get(GripType.OneLightShield).put(GripType.TwoWeaponsLight, null);
        MainGripType.get(GripType.OneLightShield).put(GripType.TwoWeaponsMedium, null);
        MainGripType.get(GripType.OneLightShield).put(GripType.TwoHandedHeavyWeapon, null);
        MainGripType.get(GripType.OneLightShield).put(GripType.TwoHandedMediumWeapon, null);
        MainGripType.get(GripType.OneLightShield).put(GripType.OneHandedHeavyWeapon, GripType.OneLightShield);
        MainGripType.get(GripType.OneLightShield).put(GripType.OneLightShield, null);
        MainGripType.get(GripType.OneLightShield).put(GripType.OneMediumShield, null);
        MainGripType.get(GripType.OneLightShield).put(GripType.OneHeavyShield, null);
        MainGripType.get(GripType.OneLightShield).put(null, OneLightShield);
        MainGripType.get(GripType.OneLightShield).put(GripType.None, OneLightShield);

        MainGripType.get(GripType.OneMediumShield).put(GripType.OneLightWeapon, OneMediumShield);
        MainGripType.get(GripType.OneMediumShield).put(GripType.OneMediumWeapon, OneMediumShield);
        MainGripType.get(GripType.OneMediumShield).put(GripType.TwoWeaponsLight, null);
        MainGripType.get(GripType.OneMediumShield).put(GripType.TwoWeaponsMedium, null);
        MainGripType.get(GripType.OneMediumShield).put(GripType.TwoHandedHeavyWeapon, null);
        MainGripType.get(GripType.OneMediumShield).put(GripType.TwoHandedMediumWeapon, null);
        MainGripType.get(GripType.OneMediumShield).put(GripType.OneHandedHeavyWeapon, GripType.OneMediumShield);
        MainGripType.get(GripType.OneMediumShield).put(GripType.OneLightShield, null);
        MainGripType.get(GripType.OneMediumShield).put(GripType.OneMediumShield, null);
        MainGripType.get(GripType.OneMediumShield).put(GripType.OneHeavyShield, null);
        MainGripType.get(GripType.OneMediumShield).put(null, OneMediumShield);
        MainGripType.get(GripType.OneMediumShield).put(GripType.None, OneMediumShield);

        MainGripType.get(GripType.OneHeavyShield).put(GripType.OneLightWeapon, OneMediumShield);
        MainGripType.get(GripType.OneHeavyShield).put(GripType.OneMediumWeapon, OneMediumShield);
        MainGripType.get(GripType.OneHeavyShield).put(GripType.TwoWeaponsLight, null);
        MainGripType.get(GripType.OneHeavyShield).put(GripType.TwoWeaponsMedium, null);
        MainGripType.get(GripType.OneHeavyShield).put(GripType.TwoHandedHeavyWeapon, null);
        MainGripType.get(GripType.OneHeavyShield).put(GripType.TwoHandedMediumWeapon, null);
        MainGripType.get(GripType.OneHeavyShield).put(GripType.OneHandedHeavyWeapon, GripType.OneMediumShield);
        MainGripType.get(GripType.OneHeavyShield).put(GripType.OneLightShield, null);
        MainGripType.get(GripType.OneHeavyShield).put(GripType.OneMediumShield, null);
        MainGripType.get(GripType.OneHeavyShield).put(GripType.OneHeavyShield, null);
        MainGripType.get(GripType.OneHeavyShield).put(null, OneHeavyShield);
        MainGripType.get(GripType.OneHeavyShield).put(GripType.None, OneHeavyShield);
    }

    public static GripType getGripType(GripType currentGripType, GripType otherGripType) {
        if (currentGripType == null) {
            return null;
        }
        if (currentGripType == None) {
            return currentGripType;
        }
        return MainGripType.get(currentGripType).get(otherGripType);
    }
    public static GripType getGripTypeByHandleType(WeaponCategory weaponCategory) {
        switch (weaponCategory) {
            case None:
                return GripType.None;
            case Medium:
                return GripType.OneMediumWeapon;
            case Light:
                return GripType.OneLightWeapon;
            case Heavy:
                return GripType.TwoHandedHeavyWeapon;
            case LightShield:
                return GripType.OneLightShield;
            case MediumShield:
                return GripType.OneMediumShield;
            case HeavyShield:
                return GripType.OneHeavyShield;
            default:
                throw new IllegalStateException("Unexpected value: " + weaponCategory);
        }
    }
}
