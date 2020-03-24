package com.loh.items.equipable.rings.head.ringModels;

import com.loh.items.Equipable;
import com.loh.items.EquipableSlot;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class RingModel extends Equipable {

    @Getter
    @Setter
    protected EquipableSlot slot = EquipableSlot.Hands;

    public RingModel() {
    }

    public RingModel(String name) {
        this.name = name;
    }
}
