package com.loh.tests.WeaponTest;

import com.loh.creatures.heroes.equipment.GripType;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class WeaponDamagePerLevel {
    @Getter @Setter
    private GripType gripType;
    @Getter @Setter
    private Integer damagePerAttack;

    public Integer getTotalDamage() {
        return 0;
    }
}
