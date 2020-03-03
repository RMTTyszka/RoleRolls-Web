package com.loh.items.armors.armorCategories;

import com.loh.items.armors.armorTypes.ArmorType;
import com.loh.shared.DefaultEntity;
import lombok.Getter;
import lombok.Setter;

@javax.persistence.Entity
public class ArmorCategory extends DefaultEntity {

    public ArmorCategory(){
        armorType = ArmorType.Light;
        defense = 0;
        evasion = 0;
        baseDefense = 0;
    }

    public ArmorCategory(ArmorType armorType, int defense, int evasion, int baseDefense) {
        this.armorType = armorType;
        this.defense = defense;
        this.evasion = evasion;
        this.baseDefense = baseDefense;
    }
    @Getter @Setter
    private ArmorType armorType;

    @Getter @Setter
    private int defense;

    @Getter @Setter
    private int evasion;

    @Getter @Setter
    private int baseDefense;

}

