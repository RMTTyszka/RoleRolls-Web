package com.loh.items.equipable.head.headpieceModel;

import com.loh.items.EquipableSlot;
import com.loh.items.EquipableTemplate;
import com.loh.items.ItemTemplateType;
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
