package com.loh.items.weapons.weaponModel;

import com.loh.items.weapons.weaponCategory.WeaponCategory;
import org.springframework.data.domain.Pageable;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.UUID;


public interface WeaponModelRepository extends PagingAndSortingRepository<WeaponModel, UUID> {
    List<WeaponModel> findAllByNameIgnoreCaseContaining(String name);
    List<WeaponModel> findAllByNameIgnoreCaseContaining(String name, Pageable paged);
    WeaponModel findTop1ByBaseWeapon_Category(WeaponCategory type);
    WeaponModel findByNameAndSystemDefaultTrue(String name);
}
