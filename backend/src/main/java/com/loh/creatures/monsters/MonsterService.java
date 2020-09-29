package com.loh.creatures.monsters;

import com.loh.creatures.equipment.EquipmentRepository;
import com.loh.creatures.equipment.GripType;
import com.loh.creatures.inventory.InventoryRepository;
import com.loh.creatures.monsters.models.MonsterModel;
import com.loh.items.equipable.armors.armorInstance.ArmorInstance;
import com.loh.items.equipable.armors.armorInstance.ArmorInstanceService;
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
import com.loh.items.equipable.weapons.weaponInstance.WeaponInstance;
import com.loh.items.equipable.weapons.weaponInstance.WeaponInstanceService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class MonsterService {


    @Autowired
    private ArmorModelRepository armorModelRepository;

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
