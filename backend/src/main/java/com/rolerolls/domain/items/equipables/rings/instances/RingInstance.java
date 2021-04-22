package com.rolerolls.domain.items.equipables.rings.instances;

import com.rolerolls.domain.items.equipables.instances.EquipableInstance;
import com.rolerolls.domain.items.equipables.rings.models.RingModel;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import javax.persistence.ManyToOne;

@Entity
@DiscriminatorValue("Ring")
public class RingInstance extends EquipableInstance {

    @Getter
    @Setter @ManyToOne
    private RingModel ringModel;

    public RingInstance() {
    }

    public RingInstance(RingModel ringModel, Integer level) {
        super(ringModel);
        this.ringModel = ringModel;
        this.setLevel(level);
        this.setName(ringModel.getName());
    }
}
