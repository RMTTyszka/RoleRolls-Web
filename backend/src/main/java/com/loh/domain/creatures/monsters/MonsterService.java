package com.loh.domain.creatures.monsters;

import com.loh.domain.creatures.equipments.EquipmentRepository;
import com.loh.domain.creatures.equipments.GripType;
import com.loh.domain.creatures.inventory.InventoryRepository;
import com.loh.domain.creatures.monsters.models.MonsterModel;
import com.loh.domain.items.equipables.armors.instances.ArmorInstance;
import com.loh.domain.items.equipables.armors.instances.ArmorInstanceService;
import com.loh.domain.items.equipables.armors.templates.ArmorTemplateRepository;
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
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class MonsterService {


    @Autowired
    private ArmorTemplateRepository armorTemplateRepository;

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
    private MonsterRepository monsterRepository;

    public Monster create(String monsterName, MonsterModel model, UUID playerId) throws Exception {

        Monster monster = new Monster(monsterName, model.getRace(), model.getRole(), playerId, playerId);
        ArmorInstance armor = armorInstanceService.instantiateNoneArmor();
        WeaponInstance weapon = weaponInstanceService.instantiateNoneWeapon();
        GloveInstance gloves = gloveInstanceService.instantiateNoneGlove();
        BeltInstance belt = beltInstanceService.instantiateNoneBelt();
        HeadpieceInstance headpiece = headpieceInstanceService.instantiateNone();
        NeckAccessoryInstance neckAccessory = neckAccessoryInstanceService.instantiateNone();
        RingInstance ringRightInstance = ringInstanceService.instantiateNone();
        RingInstance ringLeftInstance = ringInstanceService.instantiateNone();
        monster.getEquipment().equipArmor(armor);
        monster.getEquipment().equipMainWeapon(weapon, GripType.OneMediumWeapon);
        monster.getEquipment().equipGloves(gloves);
        monster.getEquipment().equipBelt(belt);
        monster.getEquipment().equipHeadpiece(headpiece);
        monster.getEquipment().equipNeckAccessory(neckAccessory);
        monster.getEquipment().equipRingRight(ringRightInstance);
        monster.getEquipment().equipRingLeft(ringLeftInstance);
        monster.getInventory().addItem(armor);
        monster.getInventory().addItem(weapon);
        monster.getInventory().addItem(gloves);
        monster.getInventory().addItem(belt);
        monster.getInventory().addItem(headpiece);
        monster.getInventory().addItem(neckAccessory);
        monster.getInventory().addItem(ringRightInstance);
        monster.getInventory().addItem(ringLeftInstance);
        monster.getInventory().setCash1(100);
        monster.setEquipment(equipmentRepository.save(monster.getEquipment()));
        monster.setInventory(inventoryRepository.save(monster.getInventory()));
        monster = monsterRepository.save(monster);
        return monster;
    }
}
