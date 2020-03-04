package com.loh.creatures.heroes;

import com.loh.creatures.Attributes;
import com.loh.creatures.heroes.equipment.EquipmentRepository;
import com.loh.creatures.heroes.inventory.InventoryRepository;
import com.loh.items.armors.DefaultArmors;
import com.loh.items.armors.armorInstance.ArmorInstance;
import com.loh.items.armors.armorInstance.ArmorInstanceRepository;
import com.loh.items.armors.armorInstance.ArmorInstanceService;
import com.loh.items.armors.armorModel.ArmorModel;
import com.loh.items.armors.armorModel.ArmorModelRepository;
import com.loh.race.Race;
import com.loh.race.RaceRepository;
import com.loh.role.Role;
import com.loh.role.RoleRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
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
    EquipmentRepository equipmentRepository;
    @Autowired
    InventoryRepository inventoryRepository;

    public void seed() throws Exception {
        if (heroRepository.findByName("Light Weapon Dummy") == null) {
            Hero hero = new Hero("Light Weapon Dummy");
            hero.setBaseAttributes(new Attributes(14, 14, 14, 12, 8, 8));

            Race race = raceRepository.findByNameAndSystemDefaultTrue("Dummy");
            Role role = roleRepository.findByNameAndSystemDefaultTrue("Dummy");

            hero.setRace(race);
            hero.setRole(role);
            ArmorModel lightArmorModel = armorModelRepository.findByNameAndSystemDefaultTrue(DefaultArmors.dummyLightArmor);
            ArmorInstance armor = armorInstanceService.InstantiateArmor(lightArmorModel, 1);
            armorInstanceRepository.save(armor);
            hero.getEquipment().equipArmor(armor);
            hero.getInventory().addItem(armor);

            hero.setEquipment(equipmentRepository.save(hero.getEquipment()));
            hero.setInventory(inventoryRepository.save(hero.getInventory()));
            inventoryRepository.save(hero.getInventory());
            equipmentRepository.save(hero.getEquipment());
            heroRepository.save(hero);
        }
    }
}
