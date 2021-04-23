package com.rolerolls.domain.items.equipables.heads.models;

import com.rolerolls.domain.items.equipables.EquipableSlot;
import com.rolerolls.domain.items.equipables.EquipableTemplate;
import com.rolerolls.domain.items.templates.ItemTemplateType;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;

@Entity
public class HeadpieceModel extends EquipableTemplate {
    protected ItemTemplateType itemTemplateType = ItemTemplateType.Head;
    @Getter
    @Setter
    protected EquipableSlot slot = EquipableSlot.Hands;

    public HeadpieceModel() {
    }

    public HeadpieceModel(String name) {
        this.name = name;
    }
}
