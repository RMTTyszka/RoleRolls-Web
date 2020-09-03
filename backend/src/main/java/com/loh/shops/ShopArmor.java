package com.loh.shops;

import com.loh.items.equipable.armors.armorModel.ArmorModel;
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

    public ShopArmor(ArmorModel armorModel, int quantity, int value) {
        this.item = armorModel;
        this.quantity = quantity;
        this.value = value;
        this.name = armorModel.getName();
        this.stack = 1;
    }
}
