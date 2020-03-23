package com.loh.items.equipable.weapons.weaponInstance;

import com.loh.items.equipable.weapons.weaponCategory.WeaponCategory;
import com.loh.items.equipable.weapons.weaponModel.WeaponModel;
import com.loh.items.equipable.weapons.weaponModel.WeaponModelRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class WeaponInstanceService {

    @Autowired
    WeaponInstanceRepository weaponInstanceRepository;
    @Autowired
    WeaponModelRepository weaponModelRepository;

    public WeaponInstance instantiateWeapon (UUID armorModelId, Integer level) {
        WeaponModel weaponModel = weaponModelRepository.findById(armorModelId).get();
        return instantiateWeapon(weaponModel, level);
    }
    public WeaponInstance instantiateWeapon(WeaponModel weaponModel, Integer level) {
        WeaponInstance weapon = new WeaponInstance(weaponModel, level);
        return weapon;
    }

    public WeaponInstance save(WeaponInstance weaponInstance) {
        this.weaponInstanceRepository.save(weaponInstance);
        return weaponInstance;
    }

    public WeaponInstance instantiateNoneWeapon() {
        WeaponModel weaponModel = weaponModelRepository.findTop1ByBaseWeapon_Category(WeaponCategory.None);
        WeaponInstance weapon = instantiateWeapon(weaponModel, 1);
        weaponInstanceRepository.save(weapon);
        return weapon;
    }
}
