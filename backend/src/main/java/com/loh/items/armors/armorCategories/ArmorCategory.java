package com.loh.items.armors.armorCategories;

import com.loh.items.armors.armorTypes.ArmorType;
import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

@javax.persistence.Entity
public class ArmorCategory extends Entity {

    public ArmorCategory(){
        Type = ArmorType.Light;
        defense = 0;
        Evasion = 0;
        BaseDefense = 0;
    }

    public ArmorCategory(ArmorCategoryEnum armorCategoryEnum, ArmorType type, int defense, int evasion, int baseDefense) {
        Type = type;
        this.defense = defense;
        Evasion = evasion;
        BaseDefense = baseDefense;
        this.armorCategoryEnum = armorCategoryEnum;
    }
    @Getter @Setter
    private ArmorType Type;

    @Getter @Setter
    private int defense;

    @Getter @Setter
    private int Evasion;

    @Getter @Setter
    private int BaseDefense;

    @Getter @Setter
    private ArmorCategoryEnum armorCategoryEnum;


}

