package com.rolerolls.domain.items.equipables.necks.models;

import com.rolerolls.domain.items.equipables.EquipableSlot;
import com.rolerolls.domain.items.equipables.EquipableTemplate;
import com.rolerolls.domain.items.templates.ItemTemplateType;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class NeckAcessoryModel extends EquipableTemplate {

    protected ItemTemplateType itemTemplateType = ItemTemplateType.Neck;
    @Getter
    @Setter
    protected EquipableSlot slot = EquipableSlot.Neck;

    public NeckAcessoryModel() {
    }

    public NeckAcessoryModel(String name) {
        this.name = name;
    }
}
