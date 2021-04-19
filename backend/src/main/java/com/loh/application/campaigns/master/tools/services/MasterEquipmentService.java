package com.loh.application.campaigns.master.tools.services;

import com.loh.domain.combats.Combat;
import com.loh.domain.combats.CombatRepository;
import com.loh.domain.creatures.Creature;
import com.loh.domain.creatures.CreatureRepository;
import com.loh.domain.items.templates.ItemTemplate;
import com.loh.domain.items.templates.ItemTemplateRepository;
import com.loh.domain.items.instances.ItemInstance;
import com.loh.domain.items.instances.ItemInstantiator;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;


@Service
public class MasterEquipmentService {

    @Autowired
    private ItemInstantiator itemInstantiator;

    @Autowired
    private ItemTemplateRepository itemTemplateRepository;
    @Autowired
    private CreatureRepository creatureRepository;
    @Autowired
    private CombatRepository combatRepository;


    public Creature InstantiateItemForCreature(UUID combatId, UUID creatureId, UUID itemTemplateId, int level, int quantity) {
        Combat combat = combatRepository.findById(combatId).get();
        ItemTemplate itemTemplate = itemTemplateRepository.findById(itemTemplateId).get();
        Creature creature = combat.getHeroes().stream().filter(e -> e.getId().equals(creatureId)).findFirst().get();

        ItemInstance itemInstance = itemInstantiator.instantiate(itemTemplate, level, quantity, true);
        creature.getInventory().addItem(itemInstance);

        creature = creatureRepository.save(creature);
        return creature;
    }
}
