package com.loh.creatures.heroes;

import com.loh.creatures.heroes.equipment.EquipmentRepository;
import com.loh.creatures.heroes.equipment.GripType;
import com.loh.creatures.heroes.inventory.InventoryRepository;
import com.loh.items.equipable.armors.armorInstance.ArmorInstance;
import com.loh.items.equipable.armors.armorInstance.ArmorInstanceRepository;
import com.loh.items.equipable.armors.armorInstance.ArmorInstanceService;
import com.loh.items.equipable.armors.armorModel.ArmorModelRepository;
import com.loh.items.equipable.gloves.gloveInstances.GloveInstance;
import com.loh.items.equipable.gloves.gloveInstances.GloveInstanceService;
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
        hero.getEquipment().equipArmor(armor);
        hero.getEquipment().equipMainWeapon(weapon, GripType.OneMediumWeapon);
        hero.getEquipment().equipGloves(gloves);
        hero.getInventory().addItem(armor);
        hero.getInventory().addItem(weapon);
        hero.getInventory().addItem(gloves);
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
