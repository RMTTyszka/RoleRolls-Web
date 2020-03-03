package com.loh.items.weapons.weaponCategory;

import com.loh.shared.DefaultEntity;
import lombok.Getter;
import lombok.Setter;

@javax.persistence.Entity
public class WeaponCategory extends DefaultEntity {
    @Getter @Setter
    private WeaponType weaponType;
    @Getter @Setter
    private WeaponHandleType handleType;


    public WeaponCategory(){
        weaponType = WeaponType.Light;
        handleType = WeaponHandleType.OneHanded;
    }

    public WeaponCategory(WeaponType weaponType, WeaponHandleType handleType) {
        this.weaponType = weaponType;
        this.handleType = handleType;
    }
}
