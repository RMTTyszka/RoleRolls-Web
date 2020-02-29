package com.loh.items.armors.armorCategories;

import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface ArmorCategoryRepository extends PagingAndSortingRepository<ArmorCategory, UUID> {

    ArmorCategory findArmorCategoryByArmorCategoryEnum(ArmorCategoryEnum armorCategoryEnum);
}
