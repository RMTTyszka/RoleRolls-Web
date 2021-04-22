package com.rolerolls.simulations.weapon;

import com.rolerolls.domain.creatures.equipments.GripType;
import com.rolerolls.domain.items.equipables.armors.categories.ArmorCategory;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class WeaponDamagePerLevel extends com.rolerolls.shared.Entity {
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
