package com.loh.creatures.heroes;

import com.loh.creatures.equipment.EquipmentRepository;
import com.loh.creatures.equipment.GripType;
import com.loh.creatures.inventory.InventoryRepository;
import com.loh.items.equipable.armors.armorInstance.ArmorInstance;
import com.loh.items.equipable.armors.armorInstance.ArmorInstanceRepository;
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
import com.loh.race.Race;
import com.loh.role.Role;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class HeroService {

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
    private HeroRepository heroRepository;


    public Hero create(String name, Race race, Role role) throws Exception {
        Hero hero = new Hero(name, race, role);

        ArmorInstance armor = armorInstanceService.instantiateNoneArmor();
        WeaponInstance weapon = weaponInstanceService.instantiateNoneWeapon();
        GloveInstance gloves = gloveInstanceService.instantiateNoneGlove();
        BeltInstance belt = beltInstanceService.instantiateNoneBelt();
        HeadpieceInstance headpiece = headpieceInstanceService.instantiateNone();
        NeckAccessoryInstance neckAccessory = neckAccessoryInstanceService.instantiateNone();
        RingInstance ringRightInstance = ringInstanceService.instantiateNone();
        RingInstance ringLeftInstance = ringInstanceService.instantiateNone();
        hero.getEquipment().equipArmor(armor);
        hero.getEquipment().equipMainWeapon(weapon, GripType.OneMediumWeapon);
        hero.getEquipment().equipGloves(gloves);
        hero.getEquipment().equipBelt(belt);
        hero.getEquipment().equipHeadpiece(headpiece);
        hero.getEquipment().equipNeckAccessory(neckAccessory);
        hero.getEquipment().equipRingRight(ringRightInstance);
        hero.getEquipment().equipRingLeft(ringLeftInstance);
        hero.getInventory().addItem(armor);
        hero.getInventory().addItem(weapon);
        hero.getInventory().addItem(gloves);
        hero.getInventory().addItem(belt);
        hero.getInventory().addItem(headpiece);
        hero.getInventory().addItem(neckAccessory);
        hero.getInventory().addItem(ringRightInstance);
        hero.getInventory().addItem(ringLeftInstance);
        hero.getInventory().setCash1(100);
        hero.setEquipment(equipmentRepository.save(hero.getEquipment()));
        hero.setInventory(inventoryRepository.save(hero.getInventory()));
        hero = heroRepository.save(hero);
        return hero;
    }
    public Hero update(Hero hero) {
        armorInstanceRepository.save(hero.getEquipment().getArmor());
        equipmentRepository.save(hero.getEquipment());
        return heroRepository.save(hero);
    }
}
