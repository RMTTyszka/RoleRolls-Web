package com.loh.domain.items.equipables.armors.instances;

import com.loh.domain.items.equipables.instances.EquipableInstance;
import com.loh.domain.items.equipables.armors.models.ArmorModel;
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

    public Integer getDefense() {
        return armorModel.getDefense(getBonus());
    }
    public Integer getEvasion() {
        return armorModel.getEvasion() + getBonus();
    }
    public Integer getDodge() {
        return armorModel != null ? armorModel.getDodge() : 0;
    }

    public ArmorInstance() {
        super();
        armorModel = new ArmorModel();
    }

    public ArmorInstance(ArmorModel armorModel, Integer level) {
        this.armorModel = armorModel;
        this.setName(armorModel.getName());
        this.setLevel(level);
    }
}
