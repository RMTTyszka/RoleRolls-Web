package com.rolerolls.domain.shops;

import com.rolerolls.domain.items.equipables.armors.templates.ArmorTemplate;
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
    protected ArmorTemplate item;

    public ShopArmor() {
    }

    public ShopArmor(ArmorTemplate armorTemplate, int quantity, int value, int level) {
        this.item = armorTemplate;
        this.quantity = quantity;
        this.value = value;
        this.setName(armorTemplate.getName());
        this.stack = 1;
        this.level = level;
    }
}
