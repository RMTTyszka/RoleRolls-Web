package com.loh.items.armors.armorInstance;

import com.loh.items.armors.armorCategories.ArmorCategory;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;


public interface ArmorInstanceRepository extends PagingAndSortingRepository<ArmorInstance, UUID> {
    ArmorInstance findByArmorModel_BaseArmor_Category(ArmorCategory category);
    ArmorInstance findByName(String name);
}
