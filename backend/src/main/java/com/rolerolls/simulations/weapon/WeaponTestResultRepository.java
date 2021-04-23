package com.rolerolls.simulations.weapon;

import com.rolerolls.domain.items.equipables.armors.categories.ArmorCategory;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface WeaponTestResultRepository extends PagingAndSortingRepository<WeaponTestResult, UUID> {
    Long deleteAllByArmorCategory(ArmorCategory armorCategory);
}