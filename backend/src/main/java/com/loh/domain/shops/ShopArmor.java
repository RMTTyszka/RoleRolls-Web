package com.loh.domain.shops;

import com.loh.domain.items.equipables.armors.models.ArmorModel;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.OneToOne;

@javax.persistence.Entity
@DiscriminatorValue("Armor")
public class ShopArmor extends ShopItem {
    @Getter
    @Setter
    @OneToOne
    protected ArmorModel item;

    public ShopArmor() {
    }

    public ShopArmor(ArmorModel armorModel, int quantity, int value, int level) {
        this.item = armorModel;
        this.quantity = quantity;
        this.value = value;
        this.setName(armorModel.getName());
        this.stack = 1;
        this.level = level;
    }
}
