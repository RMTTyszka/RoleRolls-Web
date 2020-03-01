package com.loh.items.weapons.baseWeapon;

import com.loh.items.weapons.weaponCategory.WeaponCategory;
import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.ManyToOne;

@javax.persistence.Entity
public class BaseWeapon extends Entity {
    @ManyToOne
    @Getter
    @Setter
    private WeaponCategory category;

    @Getter @Setter
    private boolean isStatic;
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
        isStatic = false;
        category = new WeaponCategory();
    }

    public static BaseWeapon DefaultBaseWeapon(String name, WeaponCategory category, String hitAtribute, String damageAttribute){
        BaseWeapon baseWeapon = new BaseWeapon(category);
        baseWeapon.name = name;
        baseWeapon.isStatic = true;
        baseWeapon.hitAttribute = hitAtribute;
        baseWeapon.damageAttribute = damageAttribute;
        return baseWeapon;
    }
}
