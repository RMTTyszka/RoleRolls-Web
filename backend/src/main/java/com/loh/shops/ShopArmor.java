package com.loh.shops;

import com.loh.items.equipable.armors.armorInstance.ArmorInstance;
import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.OneToOne;

@javax.persistence.Entity
public class ShopArmor extends Entity {
    @Getter
    @Setter
    @OneToOne
    private ArmorInstance armor;
    @Getter
    @Setter
    private int quantity;
    @Getter
    @Setter
    private int value;

    public ShopArmor() {
    }

    public ShopArmor(ArmorInstance armorInstance, int quantity, int value) {
        this.armor = armorInstance;
        this.quantity = quantity;
    }
}
