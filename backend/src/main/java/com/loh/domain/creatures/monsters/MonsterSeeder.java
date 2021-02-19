package com.loh.domain.creatures.monsters;

import com.loh.authentication.AdminSeeder;
import com.loh.authentication.User;
import com.loh.authentication.UserRepository;
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
    @Autowired
    UserRepository userRepository;
    @Autowired
    SkillsService skillsService;

    public void seed() throws Exception {
        for (int level = 1; level < 21; level++) {
            User admin = userRepository.findByEmail(AdminSeeder.adminEmail);
            if (monsterRepository.findByName(DefaultMonsters.OneLightWeapon + " Level " + level) == null) {
                Monster monster = new Monster(DefaultMonsters.OneLightWeapon + " Level " + level, admin.getId(), admin.getId());
                monster.setSkills(skillsService.save(monster.getSkills()));
                monster.setSpecialPowerMainAttribute(Attributes.Agility);
                equipArmor(monster, DefaultArmors.dummyNoneArmor);

                WeaponModel lightWeaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyLightWeapon);
                WeaponModel offHand = weaponModelRepository.findByNameAndSystemDefaultTrue("None");
                equipWeapon(monster, lightWeaponModel, true, GripType.OneLightWeapon);
                equipWeapon(monster, offHand, false, GripType.None);
                equipDummyEquipment(monster);
                saveMonsterAndLevelUp(monster, level);

            }
            if (monsterRepository.findByName(DefaultMonsters.OneMediumWeapon + " Level " + level) == null) {
                Monster monster = new Monster(DefaultMonsters.OneMediumWeapon + " Level " + level, admin.getId(), admin.getId());
                monster.setSkills(skillsService.save(monster.getSkills()));
                monster.setSpecialPowerMainAttribute(Attributes.Strength);
                equipArmor(monster, DefaultArmors.dummyNoneArmor);
                WeaponModel offHand = weaponModelRepository.findByNameAndSystemDefaultTrue("None");
                WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyMediumWeapon);
                equipWeapon(monster, weaponModel, true, GripType.OneMediumWeapon);
                equipWeapon(monster, offHand, false, GripType.None);
                equipDummyEquipment(monster);
                saveMonsterAndLevelUp(monster, level);

            }
            if (monsterRepository.findByName(DefaultMonsters.OneHeavyWeapon + " Level " + level) == null) {
                Monster monster = new Monster(DefaultMonsters.OneHeavyWeapon + " Level " + level, admin.getId(), admin.getId());
                monster.setSkills(skillsService.save(monster.getSkills()));
                monster.setSpecialPowerMainAttribute(Attributes.Strength);
                equipArmor(monster, DefaultArmors.dummyNoneArmor);
                WeaponModel offHand = weaponModelRepository.findByNameAndSystemDefaultTrue("None");
                WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue(DefaultWeapons.dummyHeavyWeapon);
                equipWeapon(monster, weaponModel, true, GripType.TwoHandedHeavyWeapon);
                equipWeapon(monster, offHand, false, GripType.None);
                equipDummyEquipment(monster);
                saveMonsterAndLevelUp(monster, level);

            }
            if (monsterRepository.findByName(DefaultMonsters.TwoLightWeapons + " Level " + level) == null) {
                Monster monster = new Monster(DefaultMonsters.TwoLightWeapons + " Level " + level, admin.getId(), admin.getId());
                monster.setSkills(skillsService.save(monster.getSkills()));
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
                Monster monster = new Monster(DefaultMonsters.TwoMediumWeapons + " Level " + level, admin.getId(), admin.getId());
                monster.setSkills(skillsService.save(monster.getSkills()));
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
                Monster monster = new Monster(DefaultMonsters.LightArmor + " Level " + level, admin.getId(), admin.getId());
                monster.setSkills(skillsService.save(monster.getSkills()));
                monster.setSpecialPowerMainAttribute(Attributes.Strength);
                equipArmor(monster, DefaultArmors.dummyLightArmor);
                equipeNoneWeapon(level, monster);
                WeaponModel offHand = weaponModelRepository.findByNameAndSystemDefaultTrue("None");
                equipWeapon(monster, offHand, false, GripType.None);  equipDummyEquipment(monster);
                saveMonsterAndLevelUp(monster, level);

            }
            if (monsterRepository.findByName(DefaultMonsters.MediumArmor + " Level " + level) == null) {
                Monster monster = new Monster(DefaultMonsters.MediumArmor + " Level " + level, admin.getId(), admin.getId());
                monster.setSkills(skillsService.save(monster.getSkills()));
                monster.setSpecialPowerMainAttribute(Attributes.Strength);
                equipArmor(monster, DefaultArmors.dummyMediumArmor);
                equipeNoneWeapon(level, monster);
                WeaponModel offHand = weaponModelRepository.findByNameAndSystemDefaultTrue("None");
                equipWeapon(monster, offHand, false, GripType.None); equipDummyEquipment(monster);
                saveMonsterAndLevelUp(monster, level);

            }
            if (monsterRepository.findByName(DefaultMonsters.HeavyArmor + " Level " + level) == null) {
                Monster monster = new Monster(DefaultMonsters.HeavyArmor + " Level " + level, admin.getId(), admin.getId());
                monster.setSkills(skillsService.save(monster.getSkills()));
                monster.setSpecialPowerMainAttribute(Attributes.Strength);
                equipArmor(monster, DefaultArmors.dummyHeavyArmor);
                equipeNoneWeapon(level, monster);
                WeaponModel offHand = weaponModelRepository.findByNameAndSystemDefaultTrue("None");
                equipWeapon(monster, offHand, false, GripType.None); equipDummyEquipment(monster);
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
        // monster = monsterRepository.save(monster);
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
