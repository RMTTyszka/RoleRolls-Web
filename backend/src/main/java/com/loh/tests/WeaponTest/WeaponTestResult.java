package com.loh.tests.WeaponTest;

import com.loh.creatures.heroes.equipment.GripType;
import com.loh.items.armors.armorCategories.ArmorCategory;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class WeaponTestResult {

    @Getter @Setter
    private GripType gripType;
    @Getter @Setter
    private ArmorCategory armorCategory;
    @Getter @Setter
    private Integer level;
    @Getter @Setter
    private Integer damage;
}
