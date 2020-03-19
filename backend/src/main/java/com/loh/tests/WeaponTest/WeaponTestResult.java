package com.loh.tests.WeaponTest;

import com.loh.creatures.heroes.equipment.GripType;
import com.loh.items.armors.armorCategories.ArmorCategory;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.Transient;
import java.util.ArrayList;
import java.util.List;

@Entity
public class WeaponTestResult extends com.loh.shared.Entity {

    @Getter @Setter
    private GripType gripType;
    @Getter @Setter
    private ArmorCategory armorCategory;
    @Getter @Setter
    private Integer level;
    @Getter @Setter
    private Integer damage;

    @Transient
    public List<Integer> damages = new ArrayList<>();

    public WeaponTestResult(GripType gripType, ArmorCategory armorCategory, Integer level) {
        this.gripType = gripType;
        this.armorCategory = armorCategory;
        this.level = level;
    }

    public WeaponTestResult() {
    }
}
