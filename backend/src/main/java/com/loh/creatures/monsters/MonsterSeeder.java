package com.loh.creatures.monsters;

import com.loh.creatures.Attributes;
import com.loh.creatures.LevelUpService;
import com.loh.creatures.equipment.EquipmentRepository;
import com.loh.creatures.equipment.GripType;
import com.loh.creatures.inventory.InventoryRepository;
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
import com.loh.items.equipable.rings.head.ringInstances.RingInstance;
import com.loh.items.equipable.rings.head.ringInstances.RingInstanceService;
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
public class MonsterSeeder {

    @Autowired
    ArmorInstanceService armorInstanceService;
    @Autowired
    MonsterRepository monsterRepository;
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

    public void seed() throws Exception {
        for (int level = 1; level < 21; level++) {
            if (level == 20) {
                System.out.println("dsad");
            }
            if (monsterRepository.findByName(DefaultMonsters.OneLightWeapon + " Level " + level) == null) {
                Monster monster = new Monster(DefaultMonsters.OneLightWeapon + " Level " + level);
                monster.setSpecialPowerMainAttribute(Attributes.Agility);
                equipArmor(monster, DefaultArmors.dummyNoneArmor);

                WeaponModel lightWeaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyLightWeapon);
                equipWeapon(monster, lightWeaponModel, true, GripType.OneLightWeapon);
                equipDummyEquipment(monster);
                saveMonsterAndLevelUp(monster, level);

            }
            if (monsterRepository.findByName(DefaultMonsters.OneMediumWeapon + " Level " + level) == null) {
                Monster monster = new Monster(DefaultMonsters.OneMediumWeapon + " Level " + level);
                monster.setSpecialPowerMainAttribute(Attributes.Strength);
                equipArmor(monster, DefaultArmors.dummyNoneArmor);

                WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyMediumWeapon);
                equipWeapon(monster, weaponModel, true, GripType.OneMediumWeapon);
                equipDummyEquipment(monster);
                saveMonsterAndLevelUp(monster, level);

            }
            if (monsterRepository.findByName(DefaultMonsters.OneHeavyWeapon + " Level " + level) == null) {
                Monster monster = new Monster(DefaultMonsters.OneHeavyWeapon + " Level " + level);
                monster.setSpecialPowerMainAttribute(Attributes.Strength);
                equipArmor(monster, DefaultArmors.dummyNoneArmor);

                WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyHeavyWeapon);
                equipWeapon(monster, weaponModel, true, GripType.TwoHandedHeavyWeapon);
                equipDummyEquipment(monster);
                saveMonsterAndLevelUp(monster, level);

            }
            if (monsterRepository.findByName(DefaultMonsters.TwoLightWeapons + " Level " + level) == null) {
                Monster monster = new Monster(DefaultMonsters.TwoLightWeapons + " Level " + level);
                monster.setSpecialPowerMainAttribute(Attributes.Agility);
                equipArmor(monster, DefaultArmors.dummyNoneArmor);

                WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyLightWeapon);
                equipWeapon(monster, weaponModel, true, GripType.TwoWeaponsLight);
                WeaponModel offWeaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyLightWeapon);
                equipWeapon(monster, offWeaponModel, false, GripType.TwoWeaponsLight);
                equipDummyEquipment(monster);
                saveMonsterAndLevelUp(monster, level);

            }
            if (monsterRepository.findByName(DefaultMonsters.TwoMediumWeapons + " Level " + level) == null) {
                Monster monster = new Monster(DefaultMonsters.TwoMediumWeapons + " Level " + level);
                monster.setSpecialPowerMainAttribute(Attributes.Strength);
                equipArmor(monster, DefaultArmors.dummyNoneArmor);

                WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyMediumWeapon);
                equipWeapon(monster, weaponModel, true, GripType.TwoWeaponsMedium);
                WeaponModel offWeaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyMediumWeapon);
                equipWeapon(monster, offWeaponModel, false, GripType.TwoWeaponsMedium);
                equipDummyEquipment(monster);
                saveMonsterAndLevelUp(monster, level);

            }
            if (monsterRepository.findByName(DefaultMonsters.LightArmor + " Level " + level) == null) {
                Monster monster = new Monster(DefaultMonsters.LightArmor + " Level " + level);
                monster.setSpecialPowerMainAttribute(Attributes.Strength);
                equipArmor(monster, DefaultArmors.dummyLightArmor);
                equipeNoneWeapon(level, monster);
                equipDummyEquipment(monster);
                saveMonsterAndLevelUp(monster, level);

            }
            if (monsterRepository.findByName(DefaultMonsters.MediumArmor + " Level " + level) == null) {
                Monster monster = new Monster(DefaultMonsters.MediumArmor + " Level " + level);
                monster.setSpecialPowerMainAttribute(Attributes.Strength);
                equipArmor(monster, DefaultArmors.dummyMediumArmor);
                equipeNoneWeapon(level, monster);
                equipDummyEquipment(monster);
                saveMonsterAndLevelUp(monster, level);

            }
            if (monsterRepository.findByName(DefaultMonsters.HeavyArmor + " Level " + level) == null) {
                Monster monster = new Monster(DefaultMonsters.HeavyArmor + " Level " + level);
                monster.setSpecialPowerMainAttribute(Attributes.Strength);
                equipArmor(monster, DefaultArmors.dummyHeavyArmor);
                equipeNoneWeapon(level, monster);
                equipDummyEquipment(monster);
                saveMonsterAndLevelUp(monster, level);
            }
        }

    }

    private void saveMonsterAndLevelUp(Monster monster, Integer level) throws Exception {
        setDummyAttributes(monster);
        monster.setEquipment(equipmentRepository.save(monster.getEquipment()));
        monster.setInventory(inventoryRepository.save(monster.getInventory()));
        inventoryRepository.save(monster.getInventory());
        equipmentRepository.save(monster.getEquipment());
        monster = monsterRepository.save(monster);
        levelUpForTest(level, monster);
    }

    private void equipDummyEquipment(Monster monster) {
        GloveInstance gloves = gloveInstanceService.instantiateDummyGlove();
        monster.getEquipment().equipGloves(gloves);
        monster.getInventory().addItem(gloves);

        BeltInstance beltInstance= beltInstanceService.instantiateDummyBelt();
        monster.getEquipment().equipBelt(beltInstance);
        monster.getInventory().addItem(beltInstance);

        HeadpieceInstance headpieceInstance= headpieceInstanceService.instantiateDummy();
        monster.getEquipment().equipHeadpiece(headpieceInstance);
        monster.getInventory().addItem(headpieceInstance);

        NeckAccessoryInstance neckAccessoryInstance = neckAccessoryInstanceService.instantiateDummy();
        monster.getEquipment().equipNeckAccessory(neckAccessoryInstance);
        monster.getInventory().addItem(neckAccessoryInstance);

        RingInstance ringRightInstance = ringInstanceService.instantiateDummy();
        monster.getEquipment().equipRingRight(ringRightInstance);
        monster.getInventory().addItem(ringRightInstance);
        RingInstance ringLeftInstance = ringInstanceService.instantiateDummy();
        monster.getEquipment().equipRingLeft(ringLeftInstance);
        monster.getInventory().addItem(ringLeftInstance);
    }

    private void equipeNoneWeapon(int level, Monster monster) throws Exception {
        WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyNoneWeapon);
        equipWeapon(monster, weaponModel, true, GripType.OneLightWeapon);
    }

    private void levelUpForTest(int level, Monster monster) {
        levelUpService.levelUpForTest(monster, level);
    }

    private void equipWeapon(Monster monster, WeaponModel weaponModel, boolean isMainWeapon, GripType gripType) throws Exception {
        WeaponInstance weapon = weaponInstanceService.instantiateWeapon(weaponModel, 1);
        weaponInstanceRepository.save(weapon);
        if (isMainWeapon) {
            monster.getEquipment().equipMainWeapon(weapon, gripType);
        } else {
            monster.getEquipment().equipOffWeapon(weapon, gripType);
        }
        monster.getInventory().addItem(weapon);

    }

    private void equipArmor(Monster monster, String dummyMediumArmor) {

        ArmorModel armorModel = armorModelRepository.findByNameAndSystemDefaultTrue(dummyMediumArmor);
        ArmorInstance armor = armorInstanceService.instantiateArmor(armorModel, 1);
        armorInstanceRepository.save(armor);
        monster.getEquipment().equipArmor(armor);
        monster.getInventory().addItem(armor);
    }
    private void setDummyAttributes(Monster monster) throws Exception {
        monster.setBaseAttributes(new Attributes(14, 14, 14, 14, 14, 14, true));
        Race race = raceRepository.findByNameAndSystemDefaultTrue("Dummy");
        Role role = roleRepository.findByNameAndSystemDefaultTrue("Dummy");
        monster.setRace(race);
        monster.setRole(role);
    }
}
