package com.rolerolls.application.items.equipables.armors.models.dtos;

import com.rolerolls.domain.items.equipables.armors.templates.ArmorTemplate;

public class InstantiateArmorByModelInput {
    public ArmorTemplate armorTemplate;
    public int level;
    public boolean shouldSave;
}
