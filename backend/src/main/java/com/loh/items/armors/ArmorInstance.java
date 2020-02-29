package com.loh.items.armors;

import com.loh.items.EquipableInstance;
import com.loh.items.armors.armorModel.ArmorModel;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.ManyToOne;
import javax.persistence.Transient;

@javax.persistence.Entity
@DiscriminatorValue("Armor")
public class ArmorInstance extends EquipableInstance {

    @Getter @Setter @ManyToOne
    private ArmorModel armorModel;
    @Transient
    public boolean isArmor = true;
}
