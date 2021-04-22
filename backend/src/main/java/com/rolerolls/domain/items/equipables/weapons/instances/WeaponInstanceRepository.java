package com.rolerolls.domain.items.equipables.weapons.instances;

import com.rolerolls.domain.items.equipables.weapons.categories.WeaponCategory;
import org.springframework.data.repository.CrudRepository;

import java.util.UUID;


public interface WeaponInstanceRepository extends CrudRepository<WeaponInstance, UUID>{
    WeaponInstance findByWeaponModel_BaseWeapon_Category(WeaponCategory category);

}
