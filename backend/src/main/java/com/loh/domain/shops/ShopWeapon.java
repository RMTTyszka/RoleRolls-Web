package com.loh.domain.shops;

import com.loh.domain.items.equipables.weapons.models.WeaponModel;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.OneToOne;

@javax.persistence.Entity
@DiscriminatorValue("Weapon")
public class ShopWeapon extends ShopItem {
    @Getter
    @Setter
    @OneToOne
    private WeaponModel weapon;

    public ShopWeapon() {
    }

    public ShopWeapon(WeaponModel weaponModel, int quantity, int value, int level) {
        this.item = weaponModel;
        this.quantity = quantity;
        this.value = value;
        this.setName(weaponModel.getName());
        this.stack = 1;
        this.level = level;
    }
}
