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

        for (DefaultWeapon weapon: DefaultWeapons.lightWeapons) {
            if (!baseWeaponRepository.existsByNameAndSystemDefaultIsTrue(weapon.name)) {
                BaseWeapon baseWeapon = BaseWeapon.DefaultBaseWeapon(weapon.name, WeaponCategory.Light, Attributes.Agility, Attributes.Agility);
                baseWeapon = baseWeaponRepository.save(baseWeapon);
                CreateModels(weapon.name, baseWeapon);
            }
        }
        for (DefaultWeapon weapon: DefaultWeapons.mediumWeapons) {
            if (!baseWeaponRepository.existsByNameAndSystemDefaultIsTrue(weapon.name)) {
                BaseWeapon baseWeapon = BaseWeapon.DefaultBaseWeapon(weapon.name, WeaponCategory.Medium, Attributes.Strength, Attributes.Strength);
                baseWeapon = baseWeaponRepository.save(baseWeapon);
                CreateModels(weapon.name, baseWeapon);
            }
        }
        for (DefaultWeapon weapon: DefaultWeapons.heavyWeapons) {
            if (!baseWeaponRepository.existsByNameAndSystemDefaultIsTrue(weapon.name)) {
                BaseWeapon baseWeapon = BaseWeapon.DefaultBaseWeapon(weapon.name, WeaponCategory.Heavy, Attributes.Strength, Attributes.Strength);
                baseWeapon = baseWeaponRepository.save(baseWeapon);
                CreateModels(weapon.name, baseWeapon);
            }
        }
        for (DefaultWeapon weapon: DefaultWeapons.lightShields) {
            if (!baseWeaponRepository.existsByNameAndSystemDefaultIsTrue(weapon.name)) {
                BaseWeapon baseWeapon = BaseWeapon.DefaultBaseWeapon(weapon.name, WeaponCategory.LightShield, Attributes.Agility, Attributes.Agility);
                baseWeapon = baseWeaponRepository.save(baseWeapon);
                CreateModels(weapon.name, baseWeapon);
            }
        }
        for (DefaultWeapon weapon: DefaultWeapons.mediumShields) {
            if (!baseWeaponRepository.existsByNameAndSystemDefaultIsTrue(weapon.name)) {
                BaseWeapon baseWeapon = BaseWeapon.DefaultBaseWeapon(weapon.name, WeaponCategory.LightShield, Attributes.Agility, Attributes.Agility);
                baseWeapon = baseWeaponRepository.save(baseWeapon);
                CreateModels(weapon.name, baseWeapon);
            }
        }
        for (DefaultWeapon weapon: DefaultWeapons.heavyShields) {
            if (!baseWeaponRepository.existsByNameAndSystemDefaultIsTrue(weapon.name)) {
                BaseWeapon baseWeapon = BaseWeapon.DefaultBaseWeapon(weapon.name, WeaponCategory.LightShield, Attributes.Agility, Attributes.Agility);
                baseWeapon = baseWeaponRepository.save(baseWeapon);
                CreateModels(weapon.name, baseWeapon);
            }
        }
        if (!baseWeaponRepository.existsByNameAndSystemDefaultIsTrue(DefaultWeapons.bareHands)) {
            BaseWeapon empty = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.bareHands, WeaponCategory.Medium, Attributes.Strength, Attributes.Strength);
            empty = baseWeaponRepository.save(empty);
            WeaponModel noneModel = new WeaponModel(DefaultWeapons.bareHands, empty);
            noneModel.setSystemDefault(true);
            weaponModelRepository.save(noneModel);
        }
        if (!baseWeaponRepository.existsByNameAndSystemDefaultIsTrue(DefaultWeapons.none)) {
            BaseWeapon none = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.none, WeaponCategory.None, Attributes.Strength, Attributes.Strength);
            none = baseWeaponRepository.save(none);
            WeaponModel noneModel = new WeaponModel(DefaultWeapons.none, none);
            noneModel.setSystemDefault(true);
            weaponModelRepository.save(noneModel);
        }
        if (!baseWeaponRepository.existsByNameAndSystemDefaultIsTrue(DefaultWeapons.dummyLightWeapon)) {
            BaseWeapon dummyLight = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyLightWeapon, WeaponCategory.Light, Attributes.Agility, Attributes.Agility);
            dummyLight = baseWeaponRepository.save(dummyLight);
            WeaponModel dummyLightWeapon = new WeaponModel(DefaultWeapons.dummyLightWeapon, dummyLight);
            dummyLightWeapon.setSystemDefault(true);
            weaponModelRepository.save(dummyLightWeapon);
        }
        if (!baseWeaponRepository.existsByNameAndSystemDefaultIsTrue(DefaultWeapons.dummyMediumWeapon)) {
            BaseWeapon dummyMedium = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyMediumWeapon, WeaponCategory.Medium, Attributes.Strength, Attributes.Strength);
            dummyMedium = baseWeaponRepository.save(dummyMedium);
            WeaponModel dummyMediumWeapon = new WeaponModel(DefaultWeapons.dummyMediumWeapon, dummyMedium);
            dummyMediumWeapon.setSystemDefault(true);
            weaponModelRepository.save(dummyMediumWeapon);
        }
        if (!baseWeaponRepository.existsByNameAndSystemDefaultIsTrue(DefaultWeapons.dummyHeavyWeapon)) {
            BaseWeapon dummyHeavy = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyHeavyWeapon, WeaponCategory.Heavy, Attributes.Strength, Attributes.Strength);
            dummyHeavy = baseWeaponRepository.save(dummyHeavy);
            WeaponModel dummyHeavyWeapon = new WeaponModel(DefaultWeapons.dummyHeavyWeapon, dummyHeavy);
            dummyHeavyWeapon.setSystemDefault(true);
            weaponModelRepository.save(dummyHeavyWeapon);
        }
        if (!baseWeaponRepository.existsByNameAndSystemDefaultIsTrue(DefaultWeapons.dummyNoneWeapon)) {
            BaseWeapon baseDummyNoneWeapon = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyNoneWeapon, WeaponCategory.None, Attributes.Strength, Attributes.Strength);
            baseDummyNoneWeapon = baseWeaponRepository.save(baseDummyNoneWeapon);
            WeaponModel dummyNoneWeapon = new WeaponModel(DefaultWeapons.dummyNoneWeapon, baseDummyNoneWeapon);
            dummyNoneWeapon.setSystemDefault(true);
            weaponModelRepository.save(dummyNoneWeapon);
        }
        if (!baseWeaponRepository.existsByNameAndSystemDefaultIsTrue(DefaultWeapons.dummyLightShield)) {
            BaseWeapon dummyLightShield = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyLightShield, WeaponCategory.LightShield, Attributes.Agility, Attributes.Agility);
            dummyLightShield = baseWeaponRepository.save(dummyLightShield);
            WeaponModel dummyLightShieldModel= new WeaponModel(DefaultWeapons.dummyLightShield, dummyLightShield);
            dummyLightShield.setSystemDefault(true);
            weaponModelRepository.save(dummyLightShieldModel);
        }
        if (!baseWeaponRepository.existsByNameAndSystemDefaultIsTrue(DefaultWeapons.dummyMediumShield)) {
            BaseWeapon dummyMediumShield = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyMediumShield, WeaponCategory.MediumShield, Attributes.Strength, Attributes.Strength);
            dummyMediumShield = baseWeaponRepository.save(dummyMediumShield);
            WeaponModel dummyLightShieldModel= new WeaponModel(DefaultWeapons.dummyMediumShield, dummyMediumShield);
            dummyLightShieldModel.setSystemDefault(true);
            weaponModelRepository.save(dummyLightShieldModel);
        }
        if (!baseWeaponRepository.existsByNameAndSystemDefaultIsTrue(DefaultWeapons.dummyHeavyShield)) {
            BaseWeapon baseDummyHeavyShield = BaseWeapon.DefaultBaseWeapon(DefaultWeapons.dummyHeavyShield, WeaponCategory.HeavyShield, Attributes.Strength, Attributes.Strength);
            baseDummyHeavyShield = baseWeaponRepository.save(baseDummyHeavyShield);
            WeaponModel dummyHeavyShield= new WeaponModel(DefaultWeapons.dummyHeavyShield, baseDummyHeavyShield);
            dummyHeavyShield.setSystemDefault(true);
            weaponModelRepository.save(dummyHeavyShield);
        }
    }

    private void CreateModels(String weaponName, BaseWeapon baseWeapon) {
        WeaponModel weapon = new WeaponModel("Common " + weaponName, baseWeapon);
        weapon.setSystemDefault(true);
        weaponModelRepository.save(weapon);
    }
}
