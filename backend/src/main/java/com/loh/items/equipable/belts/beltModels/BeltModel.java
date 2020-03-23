package com.loh.items.equipable.belts.beltModels;

import com.loh.items.Equipable;
import com.loh.items.EquipableSlot;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class BeltModel extends Equipable {

    @Getter
    @Setter
    protected EquipableSlot slot = EquipableSlot.Hands;

    public BeltModel() {
    }

    public BeltModel(String name) {
        this.name = name;
    }
}
