package com.rolerolls.domain.items.equipables.weapons.instances;

import com.rolerolls.domain.items.equipables.instances.EquipableInstance;
import com.rolerolls.domain.items.equipables.weapons.models.WeaponModel;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import javax.persistence.ManyToOne;

@Entity
@DiscriminatorValue("Weapon")
public class WeaponInstance extends EquipableInstance {


    public WeaponInstance() {
        this.weaponModel = new WeaponModel();
    }

    public WeaponInstance(WeaponModel weaponModel, Integer level, Integer quantity) {
        super(weaponModel);
        this.weaponModel = weaponModel;
        this.setName(weaponModel.getName());
        this.setQuantity(quantity);
        this.setLevel(level);
    }

    @Getter @Setter @ManyToOne
    private WeaponModel weaponModel;

}
