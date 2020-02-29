package com.loh.items.armors.baseArmor;

import com.loh.items.armors.armorCategories.ArmorCategory;
import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.ManyToOne;

@javax.persistence.Entity
public class BaseArmor extends Entity {

    @ManyToOne
    @Getter @Setter
    private ArmorCategory category;

    @Getter @Setter
    private boolean Static;
    @Getter @Setter
    private String name;

    public BaseArmor(ArmorCategory category) {
        this.category = category;
    }

    public BaseArmor() {
        name = "";
        Static = false;
        category = new ArmorCategory();
    }

    public static BaseArmor DefaultBaseArmor(String name, ArmorCategory category){
        BaseArmor baseArmor = new BaseArmor(category);
        baseArmor.name = name;
        baseArmor.Static = true;
        return baseArmor;
    }
}


