package com.loh.application.creatures;

import com.loh.application.creatures.dtos.CreatureDto;
import com.loh.application.creatures.mappers.CreatureMapper;
import com.loh.domain.creatures.Creature;
import com.loh.domain.creatures.CreatureRepository;
import com.loh.domain.creatures.equipments.EquipmentRepository;
import com.loh.domain.creatures.equipments.GripType;
import com.loh.domain.creatures.inventory.InventoryRepository;
import com.loh.domain.items.equipables.armors.instances.ArmorInstance;
import com.loh.domain.items.equipables.armors.instances.ArmorInstanceRepository;
import com.loh.domain.items.equipables.armors.instances.ArmorInstanceService;
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
import com.loh.domain.items.equipables.weapons.instances.WeaponInstance;
import com.loh.domain.items.equipables.weapons.instances.WeaponInstanceService;
import com.loh.domain.races.Race;
import com.loh.domain.roles.Role;
import com.loh.domain.skills.SkillsService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class CreaturesService {

    @Autowired
    private ArmorModelRepository armorModelRepository;
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
        GloveInstance gloves = gloveInstanceService.instantiateNoneGlove();
        BeltInstance belt = beltInstanceService.instantiateNoneBelt();
        HeadpieceInstance headpiece = headpieceInstanceService.instantiateNone();
        NeckAccessoryInstance neckAccessory = neckAccessoryInstanceService.instantiateNone();
        RingInstance ringRightInstance = ringInstanceService.instantiateNone();
        RingInstance ringLeftInstance = ringInstanceService.instantiateNone();
        creature.getEquipment().equipArmor(armor);
        creature.getEquipment().equipMainWeapon(weapon, GripType.OneMediumWeapon);
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
