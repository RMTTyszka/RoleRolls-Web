package com.loh.creatures;

import com.loh.creatures.heroes.equipment.GripType;

public class WeaponAttributes {
    public Integer damage;
    public Integer damageBonus;
    public Integer attackComplexity;
    public Integer hitBonus;

    public WeaponAttributes(GripType gripType, Integer damageAttributeBonus, Integer hitAttributeBonus, Integer weaponMagicBonus) {
        damage = gripType.getDamage();
        damageBonus = gripType.getAttributeModifier() * damageAttributeBonus + gripType.getMagicBonusModifier() * weaponMagicBonus;
        attackComplexity = gripType.getAttackComplexity();
        hitBonus = gripType.getHit() + hitAttributeBonus;
    }
}
