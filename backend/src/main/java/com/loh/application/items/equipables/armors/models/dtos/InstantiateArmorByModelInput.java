package com.loh.application.items.equipables.armors.models.dtos;

import com.loh.domain.items.equipables.armors.models.ArmorModel;

public class InstantiateArmorByModelInput {
    public ArmorModel armorTemplate;
    public int level;
    public boolean shouldSave;
}
