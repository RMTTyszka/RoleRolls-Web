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
}
