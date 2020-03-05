package com.loh.creatures.heroes;

import com.loh.creatures.heroes.equipment.EquipmentRepository;
import com.loh.creatures.heroes.inventory.InventoryRepository;
import com.loh.items.armors.armorInstance.ArmorInstanceRepository;
import com.loh.items.weapons.weaponInstance.WeaponInstanceRepository;
import com.loh.race.Race;
import com.loh.role.Role;
import com.loh.shared.BaseCrudResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class HeroService {

    @Autowired
    private ArmorInstanceRepository armorInstanceRepository;
    @Autowired
    private WeaponInstanceRepository weaponInstanceRepository;
    @Autowired
    private EquipmentRepository equipmentRepository;
    @Autowired
    private InventoryRepository inventoryRepository;
    @Autowired
    private HeroRepository heroRepository;


    public Hero create(String name, Race race, Role role) {
        Hero hero = new Hero(name, race, role);
        hero.setEquipment(equipmentRepository.save(hero.getEquipment()));
        hero.setInventory(inventoryRepository.save(hero.getInventory()));
        heroRepository.save(hero);

        BaseCrudResponse<Hero> output = new BaseCrudResponse<Hero>(true, "Successfully created hero", hero);
        return hero;
    }
    public Hero update(Hero hero) {
        armorInstanceRepository.save(hero.getEquipment().getArmor());
        equipmentRepository.save(hero.getEquipment());
        return heroRepository.save(hero);
    }
}
