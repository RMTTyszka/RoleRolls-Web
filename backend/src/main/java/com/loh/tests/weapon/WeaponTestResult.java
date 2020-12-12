package com.loh.tests.weapon;

import com.loh.domain.creatures.equipments.GripType;
import com.loh.domain.items.equipables.armors.categories.ArmorCategory;
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

    @Getter
    private Integer damage;

    @Getter @Setter
    private Integer hits;
    @Getter @Setter
    private Integer numberOfAttacks;
    private double hitsPercentage;

    public double getHitsPercentage() {
        return hitsPercentage;
    }
    public void setHitsPercentage() {
        hitsPercentage = hits != 0 && numberOfAttacks != 0 ? (double)hits / (double)numberOfAttacks : 0;
    }

    public void setDamage(Integer numberOfAttacks) {
        damage =  damages.stream().reduce(0, (a, b) -> a + b).intValue() / numberOfAttacks;
    }

    @Transient
    public List<Integer> damages = new ArrayList<>();

    public WeaponTestResult(GripType gripType, ArmorCategory armorCategory, Integer level) {
        this.gripType = gripType;
        this.armorCategory = armorCategory;
        this.level = level;
        hits = 0;
        damage = 0;
        numberOfAttacks = 0;
    }

    public WeaponTestResult() {
    }
}
