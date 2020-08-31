package com.loh.shops;

import com.loh.items.equipable.armors.armorInstance.ArmorInstance;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.OneToOne;

@javax.persistence.Entity
public class ShopArmor extends ShopItem {
    @Getter
    @Setter
    @OneToOne
    protected ArmorInstance item;

    public ShopArmor() {
    }

    public ShopArmor(ArmorInstance armorInstance, int quantity, int value) {
        this.item = armorInstance;
        this.quantity = quantity;
        this.value = value;
        this.name = armorInstance.getName();
        this.stack = 1;
    }
}
