package com.loh.items.equipable.armors.armorInstance.dtos;

import com.loh.items.equipable.armors.armorModel.ArmorModel;

public class InstantiateArmorByModelInput {
    public ArmorModel armorTemplate;
    public int level;
    public boolean shouldSave;
}
