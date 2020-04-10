package com.loh.tests.WeaponTest;

import com.loh.creatures.equipment.GripType;
import com.loh.items.equipable.armors.armorCategories.ArmorCategory;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class WeaponDamagePerLevel extends com.loh.shared.Entity {
    @Getter @Setter
    private GripType gripType;
    @Getter @Setter
    private ArmorCategory armorCategory;
    @Getter @Setter
    private Integer damagePerAttack;
    @Getter @Setter
    private Integer numberOfAttacks;
    @Getter @Setter
    private Integer hits;
    @Getter @Setter
    private double hitsPercentage;

    public Integer getTotalDamage() {
        return hits * damagePerAttack;
    }
}
