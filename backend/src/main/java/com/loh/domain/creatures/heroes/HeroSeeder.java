package com.loh.domain.creatures.heroes;

import com.loh.domain.creatures.Attributes;
import com.loh.domain.creatures.LevelUpService;
import com.loh.domain.creatures.equipments.EquipmentRepository;
import com.loh.domain.creatures.equipments.GripType;
import com.loh.domain.creatures.inventory.InventoryRepository;
import com.loh.domain.items.equipables.armors.DefaultArmors;
import com.loh.domain.items.equipables.armors.instances.ArmorInstance;
import com.loh.domain.items.equipables.armors.instances.ArmorInstanceRepository;
import com.loh.domain.items.equipables.armors.instances.ArmorInstanceService;
import com.loh.domain.items.equipables.armors.models.ArmorModel;
import com.loh.domain.items.equipables.armors.models.ArmorModelRepository;
import com.loh.domain.items.equipables.belts.instances.BeltInstance;
import com.loh.domain.items.equipables.belts.instances.BeltInstanceService;
import com.loh.domain.items.equipables.gloves.instances.GloveInstance;
import com.loh.domain.items.equipables.gloves.instances.GloveInstanceService;
import com.loh.domain.items.equipables.heads.instances.HeadpieceInstance;
import com.loh.domain.items.equipables.heads.instances.HeadpieceInstanceService;
import com.loh.domain.items.equipables.necks.instances.NeckAccessoryInstance;
import com.loh.domain.items.equipables.necks.instances.NeckAccessoryInstanceService;
import com.loh.domain.items.equipables.rings.instances.RingInstance;
import com.loh.domain.items.equipables.rings.instances.RingInstanceService;
import com.loh.domain.items.equipables.weapons.DefaultWeapons;
import com.loh.domain.items.equipables.weapons.instances.WeaponInstance;
import com.loh.domain.items.equipables.weapons.instances.WeaponInstanceRepository;
import com.loh.domain.items.equipables.weapons.instances.WeaponInstanceService;
import com.loh.domain.items.equipables.weapons.models.WeaponModel;
import com.loh.domain.items.equipables.weapons.models.WeaponModelRepository;
import com.loh.domain.races.Race;
import com.loh.domain.races.RaceRepository;
import com.loh.domain.roles.Role;
import com.loh.domain.roles.RoleRepository;
import com.loh.domain.skills.SkillsService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
@Transactional
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
    RingInstanceService ringInstanceService;
    @Autowired
    WeaponInstanceService weaponInstanceService;
    @Autowired
    EquipmentRepository equipmentRepository;
    @Autowired
    InventoryRepository inventoryRepository;
    @Autowired
    LevelUpService levelUpService;
    @Autowired
    SkillsService skillsService;

    public void seed() throws Exception {
        for (int level = 1; level < 21; level++) {
            if (heroRepository.findByName(DefaultHeroes.OneLightWeapon + " Level " + level) == null) {
                Hero hero = new Hero(DefaultHeroes.OneLightWeapon + " Level " + level);
                hero.setSkills(skillsService.save(hero.getSkills()));
                hero.setSpecialPowerMainAttribute(Attributes.Agility);
                equipArmor(hero, DefaultArmors.dummyNoneArmor);

                WeaponModel lightWeaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyLightWeapon);
                equipWeapon(hero, lightWeaponModel, true, GripType.OneLightWeapon);
                WeaponModel offHand = weaponModelRepository.findByNameAndSystemDefaultTrue("None");
                equipWeapon(hero, offHand, false, GripType.None);
                equipDummyEquipment(hero);
                saveHeroAndLevelUp(hero, level);

            }
            if (heroRepository.findByName(DefaultHeroes.OneMediumWeapon + " Level " + level) == null) {
                Hero hero = new Hero(DefaultHeroes.OneMediumWeapon + " Level " + level);
                hero.setSkills(skillsService.save(hero.getSkills()));
                hero.setSpecialPowerMainAttribute(Attributes.Strength);
                equipArmor(hero, DefaultArmors.dummyNoneArmor);

                WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyMediumWeapon);
                equipWeapon(hero, weaponModel, true, GripType.OneMediumWeapon);
                WeaponModel offHand = weaponModelRepository.findByNameAndSystemDefaultTrue("None");
                equipWeapon(hero, offHand, false, GripType.None);
                equipDummyEquipment(hero);
                saveHeroAndLevelUp(hero, level);

            }
            if (heroRepository.findByName(DefaultHeroes.OneHeavyWeapon + " Level " + level) == null) {
                Hero hero = new Hero(DefaultHeroes.OneHeavyWeapon + " Level " + level);
                hero.setSkills(skillsService.save(hero.getSkills()));
                hero.setSpecialPowerMainAttribute(Attributes.Strength);
                equipArmor(hero, DefaultArmors.dummyNoneArmor);

                WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyHeavyWeapon);
                equipWeapon(hero, weaponModel, true, GripType.TwoHandedHeavyWeapon);
                WeaponModel offHand = weaponModelRepository.findByNameAndSystemDefaultTrue("None");
                equipWeapon(hero, offHand, false, GripType.None);
                equipDummyEquipment(hero);
                saveHeroAndLevelUp(hero, level);

            }
            if (heroRepository.findByName(DefaultHeroes.TwoLightWeapons + " Level " + level) == null) {
                Hero hero = new Hero(DefaultHeroes.TwoLightWeapons + " Level " + level);
                hero.setSkills(skillsService.save(hero.getSkills()));
                hero.setSpecialPowerMainAttribute(Attributes.Agility);
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
                hero.setSkills(skillsService.save(hero.getSkills()));
                hero.setSpecialPowerMainAttribute(Attributes.Strength);
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
                hero.setSkills(skillsService.save(hero.getSkills()));
                hero.setSpecialPowerMainAttribute(Attributes.Strength);
                equipArmor(hero, DefaultArmors.dummyLightArmor);
                equipeNoneWeapon(level, hero);
                equipDummyEquipment(hero);
                saveHeroAndLevelUp(hero, level);

            }
            if (heroRepository.findByName(DefaultHeroes.MediumArmor + " Level " + level) == null) {
                Hero hero = new Hero(DefaultHeroes.MediumArmor + " Level " + level);
                hero.setSkills(skillsService.save(hero.getSkills()));
                hero.setSpecialPowerMainAttribute(Attributes.Strength);
                equipArmor(hero, DefaultArmors.dummyMediumArmor);
                equipeNoneWeapon(level, hero);
                equipDummyEquipment(hero);
                saveHeroAndLevelUp(hero, level);

            }
            if (heroRepository.findByName(DefaultHeroes.HeavyArmor + " Level " + level) == null) {
                Hero hero = new Hero(DefaultHeroes.HeavyArmor + " Level " + level);
                hero.setSkills(skillsService.save(hero.getSkills()));
                hero.setSpecialPowerMainAttribute(Attributes.Strength);
                equipArmor(hero, DefaultArmors.dummyHeavyArmor);
                equipeNoneWeapon(level, hero);
                equipDummyEquipment(hero);
                saveHeroAndLevelUp(hero, level);
            }
        }

    }

    private void saveHeroAndLevelUp(Hero hero, Integer level) throws Exception {
        setDummyAttributes(hero);
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
        hero.getEquipment().equipNeckAccessory(neckAccessoryInstance);
        hero.getInventory().addItem(neckAccessoryInstance);

        RingInstance ringRightInstance = ringInstanceService.instantiateDummy();
        hero.getEquipment().equipRingRight(ringRightInstance);
        hero.getInventory().addItem(ringRightInstance);
        RingInstance ringLeftInstance = ringInstanceService.instantiateDummy();
        hero.getEquipment().equipRingLeft(ringLeftInstance);
        hero.getInventory().addItem(ringLeftInstance);
    }

    private void equipeNoneWeapon(int level, Hero hero) throws Exception {
        WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyNoneWeapon);
        equipWeapon(hero, weaponModel, true, GripType.OneLightWeapon);
        WeaponModel offHand = weaponModelRepository.findByNameAndSystemDefaultTrue("None");
        equipWeapon(hero, offHand, false, GripType.None);
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

    private void equipArmor(Hero hero, String dummyMediumArmor) {

        ArmorModel armorModel = armorModelRepository.findByNameAndSystemDefaultTrue(dummyMediumArmor);
        ArmorInstance armor = armorInstanceService.instantiateArmor(armorModel, 1);
        armorInstanceRepository.save(armor);
        hero.getEquipment().equipArmor(armor);
        hero.getInventory().addItem(armor);
    }
    private void setDummyAttributes(Hero hero) throws Exception {
        hero.setBaseAttributes(new Attributes(14, 14, 14, 14, 14, 14, true));
        Race race = raceRepository.findByNameAndSystemDefaultTrue("Dummy");
        Role role = roleRepository.findByNameAndSystemDefaultTrue("Dummy");
        hero.setRace(race);
        hero.setRole(role);
    }
}
