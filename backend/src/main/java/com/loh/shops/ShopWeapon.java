package com.loh.shops;

import com.loh.items.equipable.weapons.weaponInstance.WeaponInstance;
import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.OneToOne;

@javax.persistence.Entity
public class ShopWeapon extends Entity {
    @Getter
    @Setter
    @OneToOne
    private WeaponInstance weapon;
    @Getter
    @Setter
    private int quantity;
}
