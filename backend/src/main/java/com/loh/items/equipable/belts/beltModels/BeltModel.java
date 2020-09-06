package com.loh.items.equipable.belts.beltModels;

import com.loh.items.EquipableSlot;
import com.loh.items.EquipableTemplate;
import com.loh.items.ItemTemplateType;
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
