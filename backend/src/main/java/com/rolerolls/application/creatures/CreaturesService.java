package com.rolerolls.application.creatures;

import com.rolerolls.application.creatures.dtos.CreatureDto;
import com.rolerolls.application.creatures.mappers.CreatureMapper;
import com.rolerolls.domain.creatures.Creature;
import com.rolerolls.domain.creatures.CreatureRepository;
import com.rolerolls.domain.creatures.equipments.EquipmentRepository;
import com.rolerolls.domain.creatures.equipments.GripType;
import com.rolerolls.domain.creatures.inventory.InventoryRepository;
import com.rolerolls.domain.items.equipables.armors.instances.ArmorInstance;
import com.rolerolls.domain.items.equipables.armors.instances.ArmorInstanceRepository;
import com.rolerolls.domain.items.equipables.armors.instances.ArmorInstanceService;
import com.rolerolls.domain.items.equipables.armors.templates.ArmorTemplateRepository;
import com.rolerolls.domain.items.equipables.belts.instances.BeltInstance;
import com.rolerolls.domain.items.equipables.belts.instances.BeltInstanceService;
import com.rolerolls.domain.items.equipables.gloves.instances.GloveInstance;
import com.rolerolls.domain.items.equipables.gloves.instances.GloveInstanceService;
import com.rolerolls.domain.items.equipables.heads.instances.HeadpieceInstance;
import com.rolerolls.domain.items.equipables.heads.instances.HeadpieceInstanceService;
import com.rolerolls.domain.items.equipables.necks.instances.NeckAccessoryInstance;
import com.rolerolls.domain.items.equipables.necks.instances.NeckAccessoryInstanceService;
import com.rolerolls.domain.items.equipables.rings.instances.RingInstance;
import com.rolerolls.domain.items.equipables.rings.instances.RingInstanceService;
import com.rolerolls.domain.items.equipables.weapons.instances.WeaponInstance;
import com.rolerolls.domain.items.equipables.weapons.instances.WeaponInstanceService;
import com.rolerolls.domain.races.Race;
import com.rolerolls.domain.roles.Role;
import com.rolerolls.domain.skills.SkillsService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class CreaturesService {

    @Autowired
    private ArmorTemplateRepository armorTemplateRepository;
    @Autowired
    private ArmorInstanceRepository armorInstanceRepository;

    @Autowired
    private ArmorInstanceService armorInstanceService;
    @Autowired
    private WeaponInstanceService weaponInstanceService;
    @Autowired
    private GloveInstanceService gloveInstanceService;
    @Autowired
    private BeltInstanceService beltInstanceService;
    @Autowired
    private HeadpieceInstanceService headpieceInstanceService;
    @Autowired
    private NeckAccessoryInstanceService neckAccessoryInstanceService;
    @Autowired
    private RingInstanceService ringInstanceService;
    @Autowired
    private EquipmentRepository equipmentRepository;
    @Autowired
    private InventoryRepository inventoryRepository;
    @Autowired
    private CreatureRepository creatureRepository;
    @Autowired
    private SkillsService skillsService;

    @Autowired
    private CreatureMapper creatureMapper;



    public Creature create(String name, Race race, Role role, Integer level, UUID playerId, UUID creatorId) throws Exception {

        Creature creature = new Creature(name, race, role, playerId, creatorId);
        while (creature.getLevel() < level){
            creature.levelUp();
        }
        creature.setSkills(skillsService.save(creature.getSkills()));
        ArmorInstance armor = armorInstanceService.instantiateNoneArmor();
        WeaponInstance weapon = weaponInstanceService.instantiateNoneWeapon();
        WeaponInstance offHandWeapon = weaponInstanceService.instantiateOffhandWeapon();
        GloveInstance gloves = gloveInstanceService.instantiateNoneGlove();
        BeltInstance belt = beltInstanceService.instantiateNoneBelt();
        HeadpieceInstance headpiece = headpieceInstanceService.instantiateNone();
        NeckAccessoryInstance neckAccessory = neckAccessoryInstanceService.instantiateNone();
        RingInstance ringRightInstance = ringInstanceService.instantiateNone();
        RingInstance ringLeftInstance = ringInstanceService.instantiateNone();
        creature.getEquipment().equipArmor(armor);
        creature.getEquipment().equipMainWeapon(weapon, GripType.OneMediumWeapon);
        creature.getEquipment().equipOffWeapon(offHandWeapon, null);
        creature.getEquipment().equipGloves(gloves);
        creature.getEquipment().equipBelt(belt);
        creature.getEquipment().equipHeadpiece(headpiece);
        creature.getEquipment().equipNeckAccessory(neckAccessory);
        creature.getEquipment().equipRingRight(ringRightInstance);
        creature.getEquipment().equipRingLeft(ringLeftInstance);
        creature.getInventory().addItem(armor);
        creature.getInventory().addItem(weapon);
        creature.getInventory().addItem(gloves);
        creature.getInventory().addItem(belt);
        creature.getInventory().addItem(headpiece);
        creature.getInventory().addItem(neckAccessory);
        creature.getInventory().addItem(ringRightInstance);
        creature.getInventory().addItem(ringLeftInstance);
        creature.getInventory().setCash1(100);
        creature.setEquipment(equipmentRepository.save(creature.getEquipment()));
        creature.setInventory(inventoryRepository.save(creature.getInventory()));
        creature = creatureRepository.save(creature);
        return creature;
    }
    public CreatureDto update(Creature creature) {
        equipmentRepository.save(creature.getEquipment());
        skillsService.save(creature.getSkills());
        creature = creatureRepository.save(creature);
        CreatureDto creatureDto = creatureMapper.map(creature);
        return creatureDto;
    }
}
