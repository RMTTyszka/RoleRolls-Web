package com.loh.items.weapons.weaponInstance;

import com.loh.items.weapons.weaponCategory.WeaponCategory;
import org.springframework.data.repository.CrudRepository;

import java.util.UUID;


public interface WeaponInstanceRepository extends CrudRepository<WeaponInstance, UUID>{
    WeaponInstance findByWeaponModel_BaseWeapon_Category(WeaponCategory category);

}
