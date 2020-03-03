package com.loh.items.weapons;

import com.loh.creatures.Attributes;
import com.loh.items.weapons.baseWeapon.BaseWeapon;
import com.loh.items.weapons.baseWeapon.BaseWeaponRepository;
import com.loh.items.weapons.weaponCategory.WeaponCategory;
import com.loh.items.weapons.weaponCategory.WeaponCategoryRepository;
import com.loh.items.weapons.weaponCategory.WeaponHandleType;
import com.loh.items.weapons.weaponCategory.WeaponType;
import com.loh.items.weapons.weaponInstance.WeaponInstance;
import com.loh.items.weapons.weaponInstance.WeaponInstanceRepository;
import com.loh.items.weapons.weaponModel.WeaponModel;
import com.loh.items.weapons.weaponModel.WeaponModelRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class WeaponSeeder {

    @Autowired
    private WeaponInstanceRepository weaponInstanceRepository;
    @Autowired
    private WeaponModelRepository weaponModelRepository;
    @Autowired
    private WeaponCategoryRepository weaponCategoryRepository;
    @Autowired
    private BaseWeaponRepository baseWeaponRepository;

    public void seed() {

        if (weaponCategoryRepository.count() <= 0) {
            WeaponCategory lightWeapon = new WeaponCategory(WeaponType.Light, WeaponHandleType.OneHanded);
            WeaponCategory mediumWeapon = new WeaponCategory(WeaponType.Medium, WeaponHandleType.OneHanded);
            WeaponCategory heavyWeapon = new WeaponCategory(WeaponType.Heavy, WeaponHandleType.TwoHanded);
            WeaponCategory noWeapon = new WeaponCategory(WeaponType.None, WeaponHandleType.OneHanded);
            WeaponCategory shield = new WeaponCategory(WeaponType.Shield, WeaponHandleType.OneHanded);
            weaponCategoryRepository.save(lightWeapon);
            weaponCategoryRepository.save(mediumWeapon);
            weaponCategoryRepository.save(heavyWeapon);
            weaponCategoryRepository.save(noWeapon);
            weaponCategoryRepository.save(shield);
        }
        if (baseWeaponRepository.count() <= 0){
            WeaponCategory heavy = weaponCategoryRepository.findWeaponCategoryByWeaponType(WeaponType.Heavy);
            WeaponCategory medium = weaponCategoryRepository.findWeaponCategoryByWeaponType(WeaponType.Medium);
            WeaponCategory light = weaponCategoryRepository.findWeaponCategoryByWeaponType(WeaponType.Light);
            WeaponCategory noneWeapon = weaponCategoryRepository.findWeaponCategoryByWeaponType(WeaponType.None);
            BaseWeapon fullPlate = BaseWeapon.DefaultBaseWeapon("Great Axe", heavy, Attributes.Strength, Attributes.Strength);
            BaseWeapon chainMail = BaseWeapon.DefaultBaseWeapon("Long Sword", medium, Attributes.Strength, Attributes.Strength);
            BaseWeapon leatherArmor = BaseWeapon.DefaultBaseWeapon("Dagger", light, Attributes.Agility, Attributes.Agility);
            BaseWeapon empty = BaseWeapon.DefaultBaseWeapon("Bare hands", noneWeapon, Attributes.Strength, Attributes.Strength);
            baseWeaponRepository.save(fullPlate);
            baseWeaponRepository.save(chainMail);
            baseWeaponRepository.save(leatherArmor);
            baseWeaponRepository.save(empty);
        }
        if (weaponModelRepository.count() <= 0){
            BaseWeapon baseNoneWeapon = baseWeaponRepository.findByCategory_WeaponType(WeaponType.None);
            WeaponModel noneModel = new WeaponModel();
            noneModel.setSystemDefault(true);
            noneModel.setBaseWeapon(baseNoneWeapon);
            noneModel.setName("Base Hands");
            weaponModelRepository.save(noneModel);
        }
        if (weaponInstanceRepository.findByWeaponModel_BaseWeapon_Category_WeaponType(WeaponType.None) == null) {
            WeaponInstance weapon = new WeaponInstance();
            WeaponModel noWeapon = weaponModelRepository.findArmorByBaseWeapon_Category_WeaponType(WeaponType.None);
            weapon.setWeaponModel(noWeapon);
            weapon.setName(noWeapon.getName());
            weaponInstanceRepository.save(weapon);
        }

    }
}
