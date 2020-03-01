package com.loh.items.weapons.weaponInstance;

import com.loh.items.EquipableInstance;
import com.loh.items.weapons.weaponModel.WeaponModel;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import javax.persistence.ManyToOne;

@Entity
@DiscriminatorValue("Weapon")
public class WeaponInstance extends EquipableInstance {

    @Getter @Setter @ManyToOne
    private WeaponModel weaponModel;

}
