package com.loh.items.equipable.neck.neckAccessoryModels;

import com.loh.items.Equipable;
import com.loh.items.EquipableSlot;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class NeckAcessoryModel extends Equipable {

    @Getter
    @Setter
    protected EquipableSlot slot = EquipableSlot.Neck;

    public NeckAcessoryModel() {
    }

    public NeckAcessoryModel(String name) {
        this.name = name;
    }
}
