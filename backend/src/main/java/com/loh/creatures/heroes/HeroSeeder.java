package com.loh.creatures.heroes;

import com.loh.creatures.Attributes;
import com.loh.creatures.DefaultHeroes;
import com.loh.creatures.LevelUpService;
import com.loh.creatures.heroes.equipment.EquipmentRepository;
import com.loh.creatures.heroes.equipment.GripType;
import com.loh.creatures.heroes.inventory.InventoryRepository;
import com.loh.items.armors.DefaultArmors;
import com.loh.items.armors.armorInstance.ArmorInstance;
import com.loh.items.armors.armorInstance.ArmorInstanceRepository;
import com.loh.items.armors.armorInstance.ArmorInstanceService;
import com.loh.items.armors.armorModel.ArmorModel;
import com.loh.items.armors.armorModel.ArmorModelRepository;
import com.loh.items.weapons.DefaultWeapons;
import com.loh.items.weapons.weaponInstance.WeaponInstance;
import com.loh.items.weapons.weaponInstance.WeaponInstanceRepository;
import com.loh.items.weapons.weaponInstance.WeaponInstanceService;
import com.loh.items.weapons.weaponModel.WeaponModel;
import com.loh.items.weapons.weaponModel.WeaponModelRepository;
import com.loh.race.Race;
import com.loh.race.RaceRepository;
import com.loh.role.Role;
import com.loh.role.RoleRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class HeroSeeder {

    @Autowired
    ArmorInstanceService armorInstanceService;
    @Autowired
    HeroRepository heroRepository;
    @Autowired
    RaceRepository raceRepository;
    @Autowired
    RoleRepository roleRepository;
    @Autowired
    ArmorInstanceRepository armorInstanceRepository;
    @Autowired
    ArmorModelRepository armorModelRepository;
    @Autowired
    WeaponModelRepository weaponModelRepository;
    @Autowired
    WeaponInstanceRepository weaponInstanceRepository;
    @Autowired
    WeaponInstanceService weaponInstanceService;
    @Autowired
    EquipmentRepository equipmentRepository;
    @Autowired
    InventoryRepository inventoryRepository;
    @Autowired
    LevelUpService levelUpService;

    public void seed() throws Exception {
        if (heroRepository.findByName(DefaultHeroes.OneLightWeapon) == null) {
            Hero hero = new Hero(DefaultHeroes.OneLightWeapon);
            equipArmor(hero, DefaultArmors.dummyNoneArmor);

            WeaponModel lightWeaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyLightWeapon);
            equipWeapon(hero, lightWeaponModel, true, GripType.OneLightWeapon);
            heroRepository.save(hero);
        }
        if (heroRepository.findByName(DefaultHeroes.OneMediumWeapon) == null) {
            Hero hero = new Hero(DefaultHeroes.OneMediumWeapon);
            equipArmor(hero, DefaultArmors.dummyNoneArmor);

            WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyMediumWeapon);
            equipWeapon(hero, weaponModel, true, GripType.OneMediumWeapon);
            heroRepository.save(hero);
        }
        if (heroRepository.findByName(DefaultHeroes.OneHeavyWeapon) == null) {
            Hero hero = new Hero(DefaultHeroes.OneHeavyWeapon);
            equipArmor(hero, DefaultArmors.dummyNoneArmor);

            WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyHeavyWeapon);
            equipWeapon(hero, weaponModel, true, GripType.TwoHandedHeavyWeapon);
            heroRepository.save(hero);
        }
        if (heroRepository.findByName(DefaultHeroes.TwoLightWeapons) == null) {
            Hero hero = new Hero(DefaultHeroes.TwoLightWeapons);
            equipArmor(hero, DefaultArmors.dummyNoneArmor);

            WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyLightWeapon);
            equipWeapon(hero, weaponModel, true, GripType.TwoWeaponsLight);
            WeaponModel offWeaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyLightWeapon);
            equipWeapon(hero, offWeaponModel, false, GripType.TwoWeaponsLight);
            heroRepository.save(hero);
        }
        if (heroRepository.findByName(DefaultHeroes.TwoMediumWeapons) == null) {
            Hero hero = new Hero(DefaultHeroes.TwoMediumWeapons);
            equipArmor(hero, DefaultArmors.dummyNoneArmor);

            WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyMediumWeapon);
            equipWeapon(hero, weaponModel, true, GripType.TwoWeaponsMedium);
            WeaponModel offWeaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyMediumWeapon);
            equipWeapon(hero, offWeaponModel, false, GripType.TwoWeaponsMedium);
            heroRepository.save(hero);
        }
        if (heroRepository.findByName(DefaultHeroes.LightArmor) == null) {
            Hero hero = new Hero(DefaultHeroes.LightArmor);
            equipArmor(hero, DefaultArmors.dummyLightArmor);

            WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyNoneWeapon);
            equipWeapon(hero, weaponModel, true, GripType.OneLightWeapon);
            heroRepository.save(hero);
        }
        if (heroRepository.findByName(DefaultHeroes.MediumArmor) == null) {
            Hero hero = new Hero(DefaultHeroes.MediumArmor);
            equipArmor(hero, DefaultArmors.dummyMediumArmor);

            WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyNoneWeapon);
            equipWeapon(hero, weaponModel, true, GripType.OneLightWeapon);
            heroRepository.save(hero);
        }
        if (heroRepository.findByName(DefaultHeroes.HeavyArmor) == null) {
            Hero hero = new Hero(DefaultHeroes.HeavyArmor);
            equipArmor(hero, DefaultArmors.dummyHeavyArmor);

            WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyNoneWeapon);
            equipWeapon(hero, weaponModel, true, GripType.OneLightWeapon);
            levelUpService.levelUpForTest(hero);
            levelUpService.levelUpForTest(hero);
            levelUpService.levelUpForTest(hero);
            heroRepository.save(hero);
        }
    }

    private void equipWeapon(Hero hero, WeaponModel weaponModel, boolean isMainWeapon, GripType gripType) throws Exception {
        WeaponInstance weapon = weaponInstanceService.instantiateWeapon(weaponModel, 1);
        weaponInstanceRepository.save(weapon);
        if (isMainWeapon) {
            hero.getEquipment().equipMainWeapon(weapon, gripType);
        } else {
            hero.getEquipment().equipOffWeapon(weapon, gripType);
        }
        hero.getInventory().addItem(weapon);

        hero.setEquipment(equipmentRepository.save(hero.getEquipment()));
        hero.setInventory(inventoryRepository.save(hero.getInventory()));
        inventoryRepository.save(hero.getInventory());
        equipmentRepository.save(hero.getEquipment());
    }

    private void equipArmor(Hero hero, String dummyMediumArmor) throws Exception {
        hero.setBaseAttributes(new Attributes(14, 14, 14, 12, 8, 8));
        Race race = raceRepository.findByNameAndSystemDefaultTrue("Dummy");
        Role role = roleRepository.findByNameAndSystemDefaultTrue("Dummy");
        hero.setRace(race);
        hero.setRole(role);
        ArmorModel armorModel = armorModelRepository.findByNameAndSystemDefaultTrue(dummyMediumArmor);
        ArmorInstance armor = armorInstanceService.instantiateArmor(armorModel, 1);
        armorInstanceRepository.save(armor);
        hero.getEquipment().equipArmor(armor);
        hero.getInventory().addItem(armor);
    }
}
