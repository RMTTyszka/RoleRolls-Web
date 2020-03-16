package com.loh.items.weapons.baseWeapon;

import com.loh.items.weapons.weaponCategory.WeaponCategory;
import org.springframework.data.domain.Pageable;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.UUID;

public interface BaseWeaponRepository extends PagingAndSortingRepository<BaseWeapon, UUID> {
    List<BaseWeapon> findAllByNameIgnoreCaseContaining(String name);
    List<BaseWeapon> findAllByNameIgnoreCaseContaining(String name, Pageable paged);
    BaseWeapon findByCategory(WeaponCategory type);
    BaseWeapon findByCategoryAndName(WeaponCategory type, String name);
    BaseWeapon findByNameAndSystemDefaultTrue(String name);
}
