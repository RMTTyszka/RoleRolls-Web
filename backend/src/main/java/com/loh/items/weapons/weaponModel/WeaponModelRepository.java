package com.loh.items.weapons.weaponModel;

import com.loh.items.weapons.weaponCategory.WeaponType;
import org.springframework.data.domain.Pageable;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.UUID;


public interface WeaponModelRepository extends PagingAndSortingRepository<WeaponModel, UUID> {
    List<WeaponModel> findAllByNameIgnoreCaseContaining(String name);
    List<WeaponModel> findAllByNameIgnoreCaseContaining(String name, Pageable paged);
    WeaponModel findArmorByBaseWeapon_Category_WeaponType(WeaponType type);
    WeaponModel findByNameAndSystemDefaultTrue(String name);
}
