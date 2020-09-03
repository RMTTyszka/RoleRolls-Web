package com.loh.items.equipable.neck.neckAccessoryModels;

import com.loh.items.EquipableSlot;
import com.loh.items.EquipableTemplate;
import com.loh.items.ItemTemplateType;
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
