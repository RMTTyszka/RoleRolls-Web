package com.rolerolls.domain.items.equipables.weapons.instances;

import com.rolerolls.domain.items.equipables.weapons.DefaultWeapons;
import com.rolerolls.domain.items.equipables.weapons.models.WeaponModel;
import com.rolerolls.domain.items.equipables.weapons.models.WeaponModelRepository;
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
    public WeaponInstance instantiateWeapon (UUID armorModelId, Integer level, Integer quantity) {
        WeaponModel weaponModel = weaponModelRepository.findById(armorModelId).get();
        return instantiateWeapon(weaponModel, level, quantity);
    }
    public WeaponInstance instantiateWeapon(WeaponModel weaponModel, Integer level, Integer quantity) {
        WeaponInstance weapon = new WeaponInstance(weaponModel, level, quantity);
        return weapon;
    }
    public WeaponInstance instantiateWeapon(WeaponModel weaponModel, Integer level) {
        WeaponInstance weapon = new WeaponInstance(weaponModel, level, 1);
        return weapon;
    }

    public WeaponInstance save(WeaponInstance weaponInstance) {
        this.weaponInstanceRepository.save(weaponInstance);
        return weaponInstance;
    }

    public WeaponInstance instantiateNoneWeapon() {
        WeaponModel weaponModel = weaponModelRepository.findTop1ByBaseWeapon_Name(DefaultWeapons.bareHands);
        WeaponInstance weapon = instantiateWeapon(weaponModel, 1);
        weapon.setRemovable(false);
        weaponInstanceRepository.save(weapon);
        return weapon;
    }
    public WeaponInstance instantiateOffhandWeapon() {
        WeaponModel weaponModel = weaponModelRepository.findTop1ByBaseWeapon_Name(DefaultWeapons.none);
        WeaponInstance weapon = instantiateWeapon(weaponModel, 1);
        weapon.setRemovable(false);
        weaponInstanceRepository.save(weapon);
        return weapon;
    }
}
