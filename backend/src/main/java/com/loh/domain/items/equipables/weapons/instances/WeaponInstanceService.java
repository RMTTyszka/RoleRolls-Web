package com.loh.domain.items.equipables.weapons.instances;

import com.loh.domain.items.equipables.weapons.models.WeaponModel;
import com.loh.domain.items.equipables.weapons.models.WeaponModelRepository;
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
        WeaponModel weaponModel = weaponModelRepository.findTop1ByBaseWeapon_Name("Bare hands");
        WeaponInstance weapon = instantiateWeapon(weaponModel, 1);
        weapon.setRemovable(false);
        weaponInstanceRepository.save(weapon);
        return weapon;
    }
}
