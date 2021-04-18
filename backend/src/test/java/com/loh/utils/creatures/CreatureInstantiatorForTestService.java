package com.loh.utils.creatures;

import com.loh.domain.creatures.Creature;
import com.loh.domain.creatures.CreatureRepository;
import com.loh.domain.creatures.equipments.Equipment;
import com.loh.domain.creatures.equipments.EquipmentRepository;
import com.loh.domain.creatures.heroes.HeroService;
import com.loh.domain.creatures.inventory.Inventory;
import com.loh.domain.creatures.inventory.InventoryRepository;
import com.loh.domain.races.Race;
import com.loh.domain.races.RaceRepository;
import com.loh.domain.roles.Role;
import com.loh.domain.roles.RoleRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class CreatureInstantiatorForTestService {

    @Autowired
    private CreatureRepository creatureRepository;
    @Autowired
    private RaceRepository raceRepository;
    @Autowired
    private RoleRepository roleRepository;
    @Autowired
    private InventoryRepository inventoryRepository;
    @Autowired
    private EquipmentRepository equipmentRepository;

    @Autowired
    private HeroService heroService;

    public Creature Instantiate() throws Exception {
        Race race = InstantiateRace();
        Role role = InstantiateRole();
        Creature creature = heroService.create("", race, role, 1, UUID.randomUUID(), UUID.randomUUID());
        return creature;
    }

    private Creature InstantiateCreature(Role role, Race race, Equipment equipment, Inventory inventory) {
        Creature creature = new Creature();
        creature.setRace(race);
        creature.setRole(role);
        creature.setEquipment(equipment);
        creature.setInventory(inventory);
        creature = creatureRepository.save(creature);
        return creature;
    }

    private Race InstantiateRace() {
        Race race = new Race();
        race = raceRepository.save(race);
        return race;
    }
    private Equipment InstantiateEquipment() {
        Equipment equipment = new Equipment();
        equipment = equipmentRepository.save(equipment);
        return equipment;
    }
    private Inventory InstantiateInventory() {
        Inventory inventory = new Inventory();
        inventory = inventoryRepository.save(inventory);
        return inventory;
    }
    private Role InstantiateRole() {
        Role role = new Role();
        role = roleRepository.save(role);
        return role;
    }
}
