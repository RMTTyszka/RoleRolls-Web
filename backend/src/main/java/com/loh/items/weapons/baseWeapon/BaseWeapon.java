package com.loh.items.weapons.baseWeapon;

import com.loh.items.weapons.weaponCategory.WeaponCategory;
import com.loh.shared.DefaultEntity;
import lombok.Getter;
import lombok.Setter;

@javax.persistence.Entity
public class BaseWeapon extends DefaultEntity {
    @Getter
    @Setter
    private WeaponCategory category;

    @Getter @Setter
    private String name;
    @Getter @Setter
    private String hitAttribute;
    @Getter @Setter
    private String damageAttribute;

    public BaseWeapon(WeaponCategory category) {
        this.category = category;
    }
    public BaseWeapon() {
        name = "";
        setSystemDefault(false);
        category = WeaponCategory.None;
    }

    public static BaseWeapon DefaultBaseWeapon(String name, WeaponCategory category, String hitAtribute, String damageAttribute){
        BaseWeapon baseWeapon = new BaseWeapon(category);
        baseWeapon.name = name;
        baseWeapon.setSystemDefault(true);
        baseWeapon.hitAttribute = hitAtribute;
        baseWeapon.damageAttribute = damageAttribute;
        return baseWeapon;
    }
}
