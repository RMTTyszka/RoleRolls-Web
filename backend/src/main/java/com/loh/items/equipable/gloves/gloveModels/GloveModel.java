package com.loh.items.equipable.gloves.gloveModels;

import com.loh.items.EquipableSlot;
import com.loh.items.EquipableTemplate;
import com.loh.items.ItemTemplateType;
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
