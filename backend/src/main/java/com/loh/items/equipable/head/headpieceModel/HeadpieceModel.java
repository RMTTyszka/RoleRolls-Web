package com.loh.items.equipable.head.headpieceModel;

import com.loh.items.Equipable;
import com.loh.items.EquipableSlot;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class HeadpieceModel extends Equipable {

    @Getter
    @Setter
    protected EquipableSlot slot = EquipableSlot.Hands;

    public HeadpieceModel() {
    }

    public HeadpieceModel(String name) {
        this.name = name;
    }
}
