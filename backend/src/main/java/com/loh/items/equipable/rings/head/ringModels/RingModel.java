package com.loh.items.equipable.rings.head.ringModels;

import com.loh.items.EquipableSlot;
import com.loh.items.EquipableTemplate;
import com.loh.items.ItemTemplateType;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class RingModel extends EquipableTemplate {
    protected ItemTemplateType itemTemplateType = ItemTemplateType.Ring;

    @Getter
    @Setter
    protected EquipableSlot slot = EquipableSlot.Hands;

    public RingModel() {
    }

    public RingModel(String name) {
        this.name = name;
    }
}
