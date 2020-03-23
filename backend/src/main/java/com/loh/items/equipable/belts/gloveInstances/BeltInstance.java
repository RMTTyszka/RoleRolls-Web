package com.loh.items.equipable.belts.gloveInstances;

import com.loh.items.EquipableInstance;
import com.loh.items.equipable.belts.gloveModels.BeltModel;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import javax.persistence.ManyToOne;

@Entity
@DiscriminatorValue("Belt")
public class BeltInstance extends EquipableInstance {

    @Getter
    @Setter @ManyToOne
    private BeltModel beltModel;

    public BeltInstance() {
    }

    public BeltInstance(BeltModel beltModel, Integer level) {
        this.beltModel = beltModel;
        this.setLevel(level);
        this.setName(beltModel.getName());
    }
}
