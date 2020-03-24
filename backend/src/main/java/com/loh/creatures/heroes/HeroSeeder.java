package com.loh.creatures.heroes;

import com.loh.creatures.Attributes;
import com.loh.creatures.DefaultHeroes;
import com.loh.creatures.LevelUpService;
import com.loh.creatures.heroes.equipment.EquipmentRepository;
import com.loh.creatures.heroes.equipment.GripType;
import com.loh.creatures.heroes.inventory.InventoryRepository;
import com.loh.items.equipable.armors.DefaultArmors;
import com.loh.items.equipable.armors.armorInstance.ArmorInstance;
import com.loh.items.equipable.armors.armorInstance.ArmorInstanceRepository;
import com.loh.items.equipable.armors.armorInstance.ArmorInstanceService;
import com.loh.items.equipable.armors.armorModel.ArmorModel;
import com.loh.items.equipable.armors.armorModel.ArmorModelRepository;
import com.loh.items.equipable.belts.beltInstances.BeltInstance;
import com.loh.items.equipable.belts.beltInstances.BeltInstanceService;
import com.loh.items.equipable.gloves.gloveInstances.GloveInstance;
import com.loh.items.equipable.gloves.gloveInstances.GloveInstanceService;
import com.loh.items.equipable.head.headpieceInstances.HeadpieceInstance;
import com.loh.items.equipable.head.headpieceInstances.HeadpieceInstanceService;
import com.loh.items.equipable.neck.neckAccessoryInstances.NeckAccessoryInstance;
import com.loh.items.equipable.neck.neckAccessoryInstances.NeckAccessoryInstanceService;
import com.loh.items.equipable.weapons.DefaultWeapons;
import com.loh.items.equipable.weapons.weaponInstance.WeaponInstance;
import com.loh.items.equipable.weapons.weaponInstance.WeaponInstanceRepository;
import com.loh.items.equipable.weapons.weaponInstance.WeaponInstanceService;
import com.loh.items.equipable.weapons.weaponModel.WeaponModel;
import com.loh.items.equipable.weapons.weaponModel.WeaponModelRepository;
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
    GloveInstanceService gloveInstanceService;
    @Autowired
    BeltInstanceService beltInstanceService;
    @Autowired
    HeadpieceInstanceService headpieceInstanceService;
    @Autowired
    NeckAccessoryInstanceService neckAccessoryInstanceService;
    @Autowired
    WeaponInstanceService weaponInstanceService;
    @Autowired
    EquipmentRepository equipmentRepository;
    @Autowired
    InventoryRepository inventoryRepository;
    @Autowired
    LevelUpService levelUpService;

    public void seed() throws Exception {
        for (int level = 1; level < 21; level++) {
            if (level == 20) {
                System.out.println("dsad");
            }
            if (heroRepository.findByName(DefaultHeroes.OneLightWeapon + " Level " + level) == null) {
                Hero hero = new Hero(DefaultHeroes.OneLightWeapon + " Level " + level);
                equipArmor(hero, DefaultArmors.dummyNoneArmor);

                WeaponModel lightWeaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyLightWeapon);
                equipWeapon(hero, lightWeaponModel, true, GripType.OneLightWeapon);
                equipDummyEquipment(hero);
                saveHeroAndLevelUp(hero, level);

            }
            if (heroRepository.findByName(DefaultHeroes.OneMediumWeapon + " Level " + level) == null) {
                Hero hero = new Hero(DefaultHeroes.OneMediumWeapon + " Level " + level);
                equipArmor(hero, DefaultArmors.dummyNoneArmor);

                WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyMediumWeapon);
                equipWeapon(hero, weaponModel, true, GripType.OneMediumWeapon);
                equipDummyEquipment(hero);
                saveHeroAndLevelUp(hero, level);

            }
            if (heroRepository.findByName(DefaultHeroes.OneHeavyWeapon + " Level " + level) == null) {
                Hero hero = new Hero(DefaultHeroes.OneHeavyWeapon + " Level " + level);
                equipArmor(hero, DefaultArmors.dummyNoneArmor);

                WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyHeavyWeapon);
                equipWeapon(hero, weaponModel, true, GripType.TwoHandedHeavyWeapon);
                equipDummyEquipment(hero);
                saveHeroAndLevelUp(hero, level);

            }
            if (heroRepository.findByName(DefaultHeroes.TwoLightWeapons + " Level " + level) == null) {
                Hero hero = new Hero(DefaultHeroes.TwoLightWeapons + " Level " + level);
                equipArmor(hero, DefaultArmors.dummyNoneArmor);

                WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyLightWeapon);
                equipWeapon(hero, weaponModel, true, GripType.TwoWeaponsLight);
                WeaponModel offWeaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyLightWeapon);
                equipWeapon(hero, offWeaponModel, false, GripType.TwoWeaponsLight);
                equipDummyEquipment(hero);
                saveHeroAndLevelUp(hero, level);

            }
            if (heroRepository.findByName(DefaultHeroes.TwoMediumWeapons + " Level " + level) == null) {
                Hero hero = new Hero(DefaultHeroes.TwoMediumWeapons + " Level " + level);
                equipArmor(hero, DefaultArmors.dummyNoneArmor);

                WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyMediumWeapon);
                equipWeapon(hero, weaponModel, true, GripType.TwoWeaponsMedium);
                WeaponModel offWeaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyMediumWeapon);
                equipWeapon(hero, offWeaponModel, false, GripType.TwoWeaponsMedium);
                equipDummyEquipment(hero);
                saveHeroAndLevelUp(hero, level);

            }
            if (heroRepository.findByName(DefaultHeroes.LightArmor + " Level " + level) == null) {
                Hero hero = new Hero(DefaultHeroes.LightArmor + " Level " + level);
                equipArmor(hero, DefaultArmors.dummyLightArmor);
                equipeNoneWeapon(level, hero);
                equipDummyEquipment(hero);
                saveHeroAndLevelUp(hero, level);

            }
            if (heroRepository.findByName(DefaultHeroes.MediumArmor + " Level " + level) == null) {
                Hero hero = new Hero(DefaultHeroes.MediumArmor + " Level " + level);
                equipArmor(hero, DefaultArmors.dummyMediumArmor);
                equipeNoneWeapon(level, hero);
                equipDummyEquipment(hero);
                saveHeroAndLevelUp(hero, level);

            }
            if (heroRepository.findByName(DefaultHeroes.HeavyArmor + " Level " + level) == null) {
                Hero hero = new Hero(DefaultHeroes.HeavyArmor + " Level " + level);
                equipArmor(hero, DefaultArmors.dummyHeavyArmor);
                equipeNoneWeapon(level, hero);
                equipDummyEquipment(hero);
                saveHeroAndLevelUp(hero, level);
            }
        }

    }

    private void saveHeroAndLevelUp(Hero hero, Integer level) {
        hero.setEquipment(equipmentRepository.save(hero.getEquipment()));
        hero.setInventory(inventoryRepository.save(hero.getInventory()));
        inventoryRepository.save(hero.getInventory());
        equipmentRepository.save(hero.getEquipment());
        hero = heroRepository.save(hero);
        levelUpForTest(level, hero);
    }

    private void equipDummyEquipment(Hero hero) {
        GloveInstance gloves = gloveInstanceService.instantiateDummyGlove();
        hero.getEquipment().equipGloves(gloves);
        hero.getInventory().addItem(gloves);

        BeltInstance beltInstance= beltInstanceService.instantiateDummyBelt();
        hero.getEquipment().equipBelt(beltInstance);
        hero.getInventory().addItem(beltInstance);

        HeadpieceInstance headpieceInstance= headpieceInstanceService.instantiateDummy();
        hero.getEquipment().equipHeadpiece(headpieceInstance);
        hero.getInventory().addItem(headpieceInstance);

        NeckAccessoryInstance neckAccessoryInstance = neckAccessoryInstanceService.instantiateDummy();
        hero.getEquipment().equipNeckAcessory(neckAccessoryInstance);
        hero.getInventory().addItem(neckAccessoryInstance);
    }

    private void equipeNoneWeapon(int level, Hero hero) throws Exception {
        WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyNoneWeapon);
        equipWeapon(hero, weaponModel, true, GripType.OneLightWeapon);
    }

    private void levelUpForTest(int level, Hero hero) {
        levelUpService.levelUpForTest(hero, level);
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
