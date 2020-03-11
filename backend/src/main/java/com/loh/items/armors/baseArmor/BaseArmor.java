package com.loh.items.armors.baseArmor;

import com.loh.items.armors.armorCategories.ArmorCategory;
import com.loh.shared.DefaultEntity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.ManyToOne;

@javax.persistence.Entity
public class BaseArmor extends DefaultEntity {

    @Getter @Setter
    private ArmorCategory category;

    @Getter @Setter
    private String name;

    public BaseArmor(ArmorCategory category) {
        this.category = category;
    }

    public BaseArmor() {
        name = "";
        setSystemDefault(false);
        category = ArmorCategory.Light;
    }

    public static BaseArmor DefaultBaseArmor(String name, ArmorCategory category){
        BaseArmor baseArmor = new BaseArmor(category);
        baseArmor.name = name;
        baseArmor.setSystemDefault(true);
        return baseArmor;
    }
}


