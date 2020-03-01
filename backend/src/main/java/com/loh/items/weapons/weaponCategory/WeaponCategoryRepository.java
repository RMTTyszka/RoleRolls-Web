package com.loh.items.weapons.weaponCategory;

import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface WeaponCategoryRepository extends PagingAndSortingRepository<WeaponCategory, UUID> {

    WeaponCategory findWeaponCategoryByWeaponType(WeaponType weaponType);
}
