package com.loh.items.weapons.weaponInstance;

import com.loh.items.weapons.weaponModel.WeaponModel;
import com.loh.items.weapons.weaponModel.WeaponModelRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class WeaponInstanceService {

    @Autowired
    WeaponInstanceRepository weaponInstanceRepository;
    @Autowired
    WeaponModelRepository weaponModelRepository;

    public WeaponInstance InstantiateWeapon(UUID weaponModelId, Integer level) {
        WeaponModel weaponModel = weaponModelRepository.findById(weaponModelId).get();
        return InstantiateWeapon(weaponModel, level);
    }
    public WeaponInstance InstantiateWeapon(WeaponModel weaponModel, Integer level) {
        WeaponInstance weapon = new WeaponInstance(weaponModel, level);
        return weapon;
    }
}
