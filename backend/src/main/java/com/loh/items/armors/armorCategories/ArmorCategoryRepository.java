package com.loh.items.armors.armorCategories;

import com.loh.items.armors.armorTypes.ArmorType;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface ArmorCategoryRepository extends PagingAndSortingRepository<ArmorCategory, UUID> {

    ArmorCategory findArmorCategoryByArmorType(ArmorType armorType);
}
