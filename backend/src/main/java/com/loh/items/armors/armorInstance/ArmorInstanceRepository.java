package com.loh.items.armors.armorInstance;

import com.loh.items.ItemInstanceRepository;
import com.loh.items.armors.armorCategories.ArmorCategory;


public interface ArmorInstanceRepository extends ItemInstanceRepository {
    ArmorInstance findByArmorModel_BaseArmor_Category(ArmorCategory category);
    ArmorInstance findByName(String name);
}
