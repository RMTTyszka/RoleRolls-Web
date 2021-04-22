package com.rolerolls.domain.items.equipables.rings.models;

import com.rolerolls.domain.items.equipables.EquipableSlot;
import com.rolerolls.domain.items.equipables.EquipableTemplate;
import com.rolerolls.domain.items.templates.ItemTemplateType;
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
