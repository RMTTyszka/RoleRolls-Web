package com.loh.items.equipable.belts.beltInstances;

import com.loh.items.EquipableInstance;
import com.loh.items.equipable.belts.beltModels.BeltModel;
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
        super();
    }

    public BeltInstance(BeltModel beltModel, Integer level) {
        super();
        this.beltModel = beltModel;
        this.setLevel(level);
        this.setName(beltModel.getName());
    }
}
