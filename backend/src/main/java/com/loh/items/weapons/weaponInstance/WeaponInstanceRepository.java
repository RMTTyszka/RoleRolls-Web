package com.loh.items.weapons.weaponInstance;

import com.loh.items.weapons.weaponCategory.WeaponType;
import org.springframework.data.repository.CrudRepository;

import java.util.UUID;


public interface WeaponInstanceRepository extends CrudRepository<WeaponInstance, UUID>{
    WeaponInstance findByWeaponModel_BaseWeapon_Category_WeaponType(WeaponType type);

}
