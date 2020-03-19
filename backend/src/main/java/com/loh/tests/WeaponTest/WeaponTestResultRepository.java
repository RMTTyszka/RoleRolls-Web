package com.loh.tests.WeaponTest;

import com.loh.items.armors.armorCategories.ArmorCategory;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface WeaponTestResultRepository extends PagingAndSortingRepository<WeaponTestResult, UUID> {
    Long deleteAllByArmorCategory(ArmorCategory armorCategory);
}