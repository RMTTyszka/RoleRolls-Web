package com.loh.items.weapons;

import com.loh.creatures.Attributes;
import com.loh.items.weapons.baseWeapon.BaseWeapon;
import com.loh.items.weapons.baseWeapon.BaseWeaponRepository;
import com.loh.items.weapons.weaponCategory.WeaponCategory;
import com.loh.items.weapons.weaponModel.WeaponModel;
import com.loh.items.weapons.weaponModel.WeaponModelRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class WeaponSeeder {

    @Autowired
    private WeaponModelRepository weaponModelRepository;
    @Autowired
    private BaseWeaponRepository baseWeaponRepository;

    public void seed() {

        if (baseWeaponRepository.count() <= 0){

            for (String weaponName: DefaultWeapons.lightWeapons) {
                BaseWeapon weapon = BaseWeapon.DefaultBaseWeapon(weaponName, WeaponCategory.Light, Attributes.Agility, Attributes.Agility);
                baseWeaponRepository.save(weapon);
            }
            for (String weaponName: DefaultWeapons.mediumWeapons) {
                BaseWeapon weapon = BaseWeapon.DefaultBaseWeapon(weaponName, WeaponCategory.Medium, Attributes.Strength, Attributes.Strength);
                baseWeaponRepository.save(weapon);
            }
            for (String weaponName: DefaultWeapons.heavyWeapons) {
                BaseWeapon weapon = BaseWeapon.DefaultBaseWeapon(weaponName, WeaponCategory.Heavy, Attributes.Strength, Attributes.Strength);
                baseWeaponRepository.save(weapon);
            }

            BaseWeapon empty = BaseWeapon.DefaultBaseWeapon("Bare hands", WeaponCategory.None, Attributes.Strength, Attributes.Strength);
            BaseWeapon dummyNone = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyNoneWeapon, WeaponCategory.None, Attributes.Strength, Attributes.Strength);
            BaseWeapon dummyLight = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyLightWeapon, WeaponCategory.Light, Attributes.Agility, Attributes.Agility);
            BaseWeapon dummyMedium = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyMediumWeapon, WeaponCategory.Medium, Attributes.Strength, Attributes.Strength);
            BaseWeapon dummyHeavy = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyHeavyWeapon, WeaponCategory.Heavy, Attributes.Strength, Attributes.Strength);
            baseWeaponRepository.save(empty);
            baseWeaponRepository.save(dummyLight);
            baseWeaponRepository.save(dummyNone);
            baseWeaponRepository.save(dummyMedium);
            baseWeaponRepository.save(dummyHeavy);


        }
        if (weaponModelRepository.count() <= 0){
            BaseWeapon baseNoneWeapon = baseWeaponRepository.findByCategory(WeaponCategory.None);
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
