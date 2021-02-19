package com.loh.domain.items.equipables.weapons;

import com.loh.domain.creatures.Attributes;
import com.loh.domain.items.equipables.weapons.base.BaseWeapon;
import com.loh.domain.items.equipables.weapons.base.BaseWeaponRepository;
import com.loh.domain.items.equipables.weapons.categories.WeaponCategory;
import com.loh.domain.items.equipables.weapons.models.WeaponModel;
import com.loh.domain.items.equipables.weapons.models.WeaponModelRepository;
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
            for (String weaponName: DefaultWeapons.lightShields) {
                BaseWeapon weapon = BaseWeapon.DefaultBaseWeapon(weaponName, WeaponCategory.LightShield, Attributes.Agility, Attributes.Agility);
                baseWeaponRepository.save(weapon);
            }
            for (String weaponName: DefaultWeapons.mediumShields) {
                BaseWeapon weapon = BaseWeapon.DefaultBaseWeapon(weaponName, WeaponCategory.MediumShield, Attributes.Strength, Attributes.Strength);
                baseWeaponRepository.save(weapon);
            }
            for (String weaponName: DefaultWeapons.heavyShields) {
                BaseWeapon weapon = BaseWeapon.DefaultBaseWeapon(weaponName, WeaponCategory.HeavyShield, Attributes.Strength, Attributes.Strength);
                baseWeaponRepository.save(weapon);
            }

            BaseWeapon empty = BaseWeapon.DefaultBaseWeapon("Bare hands", WeaponCategory.Medium, Attributes.Strength, Attributes.Strength);
            BaseWeapon none = BaseWeapon.DefaultBaseWeapon("None", WeaponCategory.None, Attributes.Strength, Attributes.Strength);
            BaseWeapon dummyNone = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyNoneWeapon, WeaponCategory.None, Attributes.Strength, Attributes.Strength);
            BaseWeapon dummyLight = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyLightWeapon, WeaponCategory.Light, Attributes.Agility, Attributes.Agility);
            BaseWeapon dummyMedium = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyMediumWeapon, WeaponCategory.Medium, Attributes.Strength, Attributes.Strength);
            BaseWeapon dummyHeavy = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyHeavyWeapon, WeaponCategory.Heavy, Attributes.Strength, Attributes.Strength);
            BaseWeapon dummyLightShield = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyLightShield, WeaponCategory.LightShield, Attributes.Agility, Attributes.Agility);
            BaseWeapon dummyMediumShield = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyMediumShield, WeaponCategory.MediumShield, Attributes.Strength, Attributes.Strength);
            BaseWeapon dummyHeavyShield = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyHeavyShield, WeaponCategory.HeavyShield, Attributes.Strength, Attributes.Strength);
            baseWeaponRepository.save(empty);
            baseWeaponRepository.save(none);
            baseWeaponRepository.save(dummyNone);
            baseWeaponRepository.save(dummyLight);
            baseWeaponRepository.save(dummyMedium);
            baseWeaponRepository.save(dummyHeavy);
            baseWeaponRepository.save(dummyLightShield);
            baseWeaponRepository.save(dummyMediumShield);
            baseWeaponRepository.save(dummyHeavyShield);


        }
        if (weaponModelRepository.count() <= 0){
            BaseWeapon baseNoneWeapon = baseWeaponRepository.findByCategoryAndName(WeaponCategory.Medium, "Bare hands");
            BaseWeapon baseEmptyWeapon = baseWeaponRepository.findByCategoryAndName(WeaponCategory.None, "None");
            WeaponModel noneModel = new WeaponModel("Bare Hands", baseNoneWeapon);
            WeaponModel emptyModel = new WeaponModel("None", baseEmptyWeapon);
            noneModel.setSystemDefault(true);
            emptyModel.setSystemDefault(true);
            weaponModelRepository.save(noneModel);
            weaponModelRepository.save(emptyModel);

            for (String weaponName: DefaultWeapons.lightWeapons) {
                CreateModels(weaponName);
            }
            for (String weaponName: DefaultWeapons.mediumWeapons) {
                CreateModels(weaponName);
            }
            for (String weaponName: DefaultWeapons.heavyWeapons) {
                CreateModels(weaponName);
            }
            for (String weaponName: DefaultWeapons.lightShields) {
                CreateModels(weaponName);
            }
            for (String weaponName: DefaultWeapons.mediumShields) {
                CreateModels(weaponName);
            }
            for (String weaponName: DefaultWeapons.heavyShields) {
                CreateModels(weaponName);
            }

            BaseWeapon baseDummyLightWeapon = baseWeaponRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyLightWeapon);
            BaseWeapon baseDummyMediumWeapon = baseWeaponRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyMediumWeapon);
            BaseWeapon baseDummyHeavyWeapon = baseWeaponRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyHeavyWeapon);
            BaseWeapon baseDummyNoneWeapon = baseWeaponRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyNoneWeapon);
            BaseWeapon baseDummyLightShield = baseWeaponRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyLightShield);
            BaseWeapon baseDummyMediumShield = baseWeaponRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyMediumShield);
            BaseWeapon baseDummyHeavyShield = baseWeaponRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyHeavyShield);
            WeaponModel dummyNoneWeapon = new WeaponModel(DefaultWeapons.dummyNoneWeapon, baseDummyNoneWeapon);
            WeaponModel dummyLightWeapon = new WeaponModel(DefaultWeapons.dummyLightWeapon, baseDummyLightWeapon);
            WeaponModel dummyMediumWeapon = new WeaponModel(DefaultWeapons.dummyMediumWeapon, baseDummyMediumWeapon);
            WeaponModel dummyHeavyWeapon = new WeaponModel(DefaultWeapons.dummyHeavyWeapon, baseDummyHeavyWeapon);
            WeaponModel dummyLightShield= new WeaponModel(DefaultWeapons.dummyLightShield, baseDummyLightShield);
            WeaponModel dummyMediumShield= new WeaponModel(DefaultWeapons.dummyMediumShield, baseDummyMediumShield);
            WeaponModel dummyHeavyShield= new WeaponModel(DefaultWeapons.dummyHeavyShield, baseDummyHeavyShield);
            dummyNoneWeapon.setSystemDefault(true);
            dummyLightWeapon.setSystemDefault(true);
            dummyMediumWeapon.setSystemDefault(true);
            dummyHeavyWeapon.setSystemDefault(true);
            dummyLightShield.setSystemDefault(true);
            dummyMediumShield.setSystemDefault(true);
            dummyHeavyShield.setSystemDefault(true);

            weaponModelRepository.save(dummyNoneWeapon);
            weaponModelRepository.save(dummyLightWeapon);
            weaponModelRepository.save(dummyMediumWeapon);
            weaponModelRepository.save(dummyHeavyWeapon);
            weaponModelRepository.save(dummyLightShield);
            weaponModelRepository.save(dummyMediumShield);
            weaponModelRepository.save(dummyHeavyShield);
        }
    }

    private void CreateModels(String weaponName) {
        BaseWeapon baseWeapon = baseWeaponRepository.findByNameAndSystemDefaultTrue(weaponName);
        WeaponModel weapon = new WeaponModel("Common " + weaponName, baseWeapon);
        weapon.setSystemDefault(true);
        weaponModelRepository.save(weapon);
    }
}
