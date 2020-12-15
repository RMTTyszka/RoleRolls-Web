package com.loh.domain.items.equipables.belts.instances;

import com.loh.domain.items.equipables.instances.EquipableInstance;
import com.loh.domain.items.equipables.belts.models.BeltModel;
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
