package com.loh.domain.items.equipables.weapons.base;

import com.loh.domain.creatures.Attributes;
import com.loh.domain.items.equipables.weapons.categories.WeaponCategory;
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
        hitAttribute = Attributes.Strength;
        damageAttribute = Attributes.Strength;
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
