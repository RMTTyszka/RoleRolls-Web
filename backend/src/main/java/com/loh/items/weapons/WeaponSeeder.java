package com.loh.items.weapons;

import com.loh.creatures.Attributes;
import com.loh.items.weapons.baseWeapon.BaseWeapon;
import com.loh.items.weapons.baseWeapon.BaseWeaponRepository;
import com.loh.items.weapons.weaponCategory.WeaponCategory;
import com.loh.items.weapons.weaponCategory.WeaponCategoryRepository;
import com.loh.items.weapons.weaponCategory.WeaponHandleType;
import com.loh.items.weapons.weaponCategory.WeaponType;
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

            for (String weaponName: DefaultWeapons.lightWeapons) {
                BaseWeapon weapon = BaseWeapon.DefaultBaseWeapon(weaponName, light, Attributes.Agility, Attributes.Agility);
                baseWeaponRepository.save(weapon);
            }
            for (String weaponName: DefaultWeapons.mediumWeapons) {
                BaseWeapon weapon = BaseWeapon.DefaultBaseWeapon(weaponName, medium, Attributes.Strength, Attributes.Strength);
                baseWeaponRepository.save(weapon);
            }
            for (String weaponName: DefaultWeapons.heavyWeapons) {
                BaseWeapon weapon = BaseWeapon.DefaultBaseWeapon(weaponName, heavy, Attributes.Strength, Attributes.Strength);
                baseWeaponRepository.save(weapon);
            }

            BaseWeapon empty = BaseWeapon.DefaultBaseWeapon("Bare hands", noneWeapon, Attributes.Strength, Attributes.Strength);
            BaseWeapon dummyNone = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyNoneWeapon, light, Attributes.Strength, Attributes.Strength);
            BaseWeapon dummyLight = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyLightWeapon, light, Attributes.Agility, Attributes.Agility);
            BaseWeapon dummyMedium = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyMediumWeapon, medium, Attributes.Strength, Attributes.Strength);
            BaseWeapon dummyHeavy = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyHeavyWeapon, heavy, Attributes.Strength, Attributes.Strength);
            baseWeaponRepository.save(empty);
            baseWeaponRepository.save(dummyLight);
            baseWeaponRepository.save(dummyNone);
            baseWeaponRepository.save(dummyMedium);
            baseWeaponRepository.save(dummyHeavy);


        }
        if (weaponModelRepository.count() <= 0){
            BaseWeapon baseNoneWeapon = baseWeaponRepository.findByCategory_WeaponType(WeaponType.None);
            WeaponModel noneModel = new WeaponModel("Bare Hands", baseNoneWeapon);
            noneModel.setSystemDefault(true);
            weaponModelRepository.save(noneModel);

            for (String weaponName: DefaultWeapons.lightWeapons) {
                BaseWeapon baseWeapon = baseWeaponRepository.findByNameAndSystemDefaultTrue(weaponName);
                WeaponModel weapon = new WeaponModel("Common " + weaponName, baseWeapon);
                weapon.setSystemDefault(true);
                weaponModelRepository.save(weapon);
            }
            for (String weaponName: DefaultWeapons.mediumWeapons) {
                BaseWeapon baseWeapon = baseWeaponRepository.findByNameAndSystemDefaultTrue(weaponName);
                WeaponModel weapon = new WeaponModel("Common " + weaponName, baseWeapon);
                weapon.setSystemDefault(true);
                weaponModelRepository.save(weapon);
            }
            for (String weaponName: DefaultWeapons.heavyWeapons) {
                BaseWeapon baseWeapon = baseWeaponRepository.findByNameAndSystemDefaultTrue(weaponName);
                WeaponModel weapon = new WeaponModel("Common " + weaponName, baseWeapon);
                weapon.setSystemDefault(true);
                weaponModelRepository.save(weapon);
            }

            BaseWeapon baseDummyLightWeapon = baseWeaponRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyLightWeapon);
            BaseWeapon baseDummyMediumWeapon = baseWeaponRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyMediumWeapon);
            BaseWeapon baseDummyHeavyWeapon = baseWeaponRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyHeavyWeapon);
            BaseWeapon baseDummyNoneWeapon = baseWeaponRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyNoneWeapon);
            WeaponModel dummyNoneWeapon = new WeaponModel(DefaultWeapons.dummyNoneWeapon, baseDummyNoneWeapon);
            WeaponModel dummyLightWeapon = new WeaponModel(DefaultWeapons.dummyLightWeapon, baseDummyLightWeapon);
            WeaponModel dummyMediumWeapon = new WeaponModel(DefaultWeapons.dummyMediumWeapon, baseDummyMediumWeapon);
            WeaponModel dummyHeavyWeapon = new WeaponModel(DefaultWeapons.dummyHeavyWeapon, baseDummyHeavyWeapon);
            dummyNoneWeapon.setSystemDefault(true);
            dummyLightWeapon.setSystemDefault(true);
            dummyMediumWeapon.setSystemDefault(true);
            dummyHeavyWeapon.setSystemDefault(true);

            weaponModelRepository.save(dummyNoneWeapon);
            weaponModelRepository.save(dummyLightWeapon);
            weaponModelRepository.save(dummyMediumWeapon);
            weaponModelRepository.save(dummyHeavyWeapon);
        }
    }
}
