package com.loh.items.weapons.weaponCategory;

import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

@javax.persistence.Entity
public class WeaponCategory extends Entity {
    @Getter @Setter
    private WeaponType Type;
    @Getter @Setter
    private WeaponHandleType handleType;
    @Getter @Setter
    private String hitAttribute;
    @Getter @Setter
    private String damageAttribute;
    @Getter @Setter
    private Integer damageAttributeModifier;
    @Getter @Setter
    private Integer damageMagicBonusModifier;
}
