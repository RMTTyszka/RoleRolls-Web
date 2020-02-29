package com.loh.items.armors;

import com.loh.items.armors.armorCategories.ArmorCategoryEnum;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;


public interface ArmorRepository extends PagingAndSortingRepository<ArmorInstance, UUID> {
    ArmorInstance findByArmorModel_BaseArmor_Category_ArmorCategoryEnum(ArmorCategoryEnum category);
}
