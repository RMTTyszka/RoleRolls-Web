package com.loh.creatures;

import com.loh.creatures.equipment.GripType;
import lombok.Getter;

public class WeaponAttributes {
    @Getter
    private Integer damage;
    @Getter
    private Integer damageBonus;
    @Getter
    private Integer attackComplexity;
    @Getter
    private Integer hitBonus;

    public WeaponAttributes(GripType gripType, Integer damageAttributeBonus, Integer hitBonus, Integer weaponMagicBonus, GripType otherHandGripType) {
        damage = gripType.getDamage();
        damageBonus = gripType.getAttributeModifier() * damageAttributeBonus + gripType.getMagicBonusModifier() * weaponMagicBonus;
        attackComplexity = gripType.getAttackComplexity();
        this.hitBonus = gripType.getHit() + hitBonus + weaponMagicBonus + (otherHandGripType != null ? otherHandGripType.getShieldHitBonus() : 0);
    }

    public WeaponAttributes() {
        damage = 0;
        damageBonus = 0;
        attackComplexity = 0;
        hitBonus = 0;
    }
}
