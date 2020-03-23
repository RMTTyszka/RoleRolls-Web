package com.loh.items.equipable.gloves.gloveModels;

import com.loh.items.Equipable;
import com.loh.items.EquipableSlot;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class GloveModel extends Equipable {

    @Getter
    @Setter
    protected EquipableSlot slot = EquipableSlot.Hands;

    public GloveModel() {
    }

    public GloveModel(String name) {
        this.name = name;
    }
}
