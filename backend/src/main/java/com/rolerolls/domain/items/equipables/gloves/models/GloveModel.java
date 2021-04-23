package com.rolerolls.domain.items.equipables.gloves.models;

import com.rolerolls.domain.items.equipables.EquipableSlot;
import com.rolerolls.domain.items.equipables.EquipableTemplate;
import com.rolerolls.domain.items.templates.ItemTemplateType;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class GloveModel extends EquipableTemplate {
    protected ItemTemplateType itemTemplateType = ItemTemplateType.Glove;
    @Getter
    @Setter
    protected EquipableSlot slot = EquipableSlot.Hands;

    public GloveModel() {
    }

    public GloveModel(String name) {
        this.name = name;
    }
}
