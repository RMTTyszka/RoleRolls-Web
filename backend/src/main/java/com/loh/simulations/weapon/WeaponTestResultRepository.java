package com.loh.simulations.weapon;

import com.loh.domain.items.equipables.armors.categories.ArmorCategory;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface WeaponTestResultRepository extends PagingAndSortingRepository<WeaponTestResult, UUID> {
    Long deleteAllByArmorCategory(ArmorCategory armorCategory);
}