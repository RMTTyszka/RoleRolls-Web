package com.rolerolls.domain.items.equipables.belts.models;

import com.rolerolls.domain.items.equipables.EquipableSlot;
import com.rolerolls.domain.items.equipables.EquipableTemplate;
import com.rolerolls.domain.items.templates.ItemTemplateType;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class BeltModel extends EquipableTemplate {
    protected ItemTemplateType itemTemplateType = ItemTemplateType.Belt;
    @Getter
    @Setter
    protected EquipableSlot slot = EquipableSlot.Hands;

    public BeltModel() {
    }

    public BeltModel(String name) {
        this.name = name;
    }
}
