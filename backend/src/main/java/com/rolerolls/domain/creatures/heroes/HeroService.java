package com.rolerolls.domain.creatures.heroes;

import com.rolerolls.domain.creatures.equipments.EquipmentRepository;
import com.rolerolls.domain.creatures.equipments.GripType;
import com.rolerolls.domain.creatures.inventory.InventoryRepository;
import com.rolerolls.domain.items.equipables.armors.instances.ArmorInstance;
import com.rolerolls.domain.items.equipables.armors.instances.ArmorInstanceService;
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
public class HeroService {

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
    @Autowired
    private SkillsService skillsService;


    public Hero create(String name, Race race, Role role, Integer level, UUID playerId, UUID creatorId) throws Exception {

        Hero hero = new Hero(name, race, role, playerId, creatorId);
        while (hero.getLevel() < level){
            hero.levelUp();
        }
        hero.setSkills(skillsService.save(hero.getSkills()));
        ArmorInstance armor = armorInstanceService.instantiateNoneArmor();
        WeaponInstance weapon = weaponInstanceService.instantiateNoneWeapon();
        WeaponInstance offHandWeapon = weaponInstanceService.instantiateOffhandWeapon();
        GloveInstance gloves = gloveInstanceService.instantiateNoneGlove();
        BeltInstance belt = beltInstanceService.instantiateNoneBelt();
        HeadpieceInstance headpiece = headpieceInstanceService.instantiateNone();
        NeckAccessoryInstance neckAccessory = neckAccessoryInstanceService.instantiateNone();
        RingInstance ringRightInstance = ringInstanceService.instantiateNone();
        RingInstance ringLeftInstance = ringInstanceService.instantiateNone();
        hero.getEquipment().equipArmor(armor);
        hero.getEquipment().equipMainWeapon(weapon, GripType.OneMediumWeapon);
        hero.getEquipment().equipOffWeapon(offHandWeapon, GripType.None);
        hero.getEquipment().equipGloves(gloves);
        hero.getEquipment().equipBelt(belt);
        hero.getEquipment().equipHeadpiece(headpiece);
        hero.getEquipment().equipNeckAccessory(neckAccessory);
        hero.getEquipment().equipRingRight(ringRightInstance);
        hero.getEquipment().equipRingLeft(ringLeftInstance);
        hero.getInventory().setCash1(100);
        hero.setEquipment(equipmentRepository.save(hero.getEquipment()));
        hero.setInventory(inventoryRepository.save(hero.getInventory()));
        hero = heroRepository.save(hero);
        return hero;
    }
    public Hero update(Hero hero) {
        equipmentRepository.save(hero.getEquipment());
        skillsService.save(hero.getSkills());
        return heroRepository.save(hero);
    }
}
