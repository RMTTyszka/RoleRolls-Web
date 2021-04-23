package com.rolerolls.application.campaigns.master.tools.services;

import com.rolerolls.domain.campaigns.Campaign;
import com.rolerolls.domain.campaigns.CampaignRepository;
import com.rolerolls.domain.combats.Combat;
import com.rolerolls.domain.combats.CombatRepository;
import com.rolerolls.domain.creatures.Creature;
import com.rolerolls.domain.creatures.CreatureRepository;
import com.rolerolls.domain.items.instances.ItemInstance;
import com.rolerolls.domain.items.instances.ItemInstantiator;
import com.rolerolls.domain.items.templates.ItemTemplate;
import com.rolerolls.domain.items.templates.ItemTemplateRepository;
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
    @Autowired
    private CampaignRepository campaignRepository;


    public Creature InstantiateItemForCreatureFromCampaign(UUID campaignId, UUID creatureId, UUID itemTemplateId, int level, int quantity) {
        Campaign combat = campaignRepository.findById(campaignId).get();
        ItemTemplate itemTemplate = itemTemplateRepository.findById(itemTemplateId).get();
        Creature creature = combat.getHeroes().stream().filter(e -> e.getId().equals(creatureId)).findFirst().get();

        ItemInstance itemInstance = itemInstantiator.instantiate(itemTemplate, level, quantity, true);
        creature.getInventory().addItem(itemInstance);

        creature = creatureRepository.save(creature);
        return creature;
    }
    public Creature InstantiateItemForCreatureFromCombat(UUID combatId, UUID creatureId, UUID itemTemplateId, int level, int quantity) {
        Combat combat = combatRepository.findById(combatId).get();
        ItemTemplate itemTemplate = itemTemplateRepository.findById(itemTemplateId).get();
        Creature creature = combat.getHeroes().stream().filter(e -> e.getId().equals(creatureId)).findFirst().get();

        ItemInstance itemInstance = itemInstantiator.instantiate(itemTemplate, level, quantity, true);
        creature.getInventory().addItem(itemInstance);

        creature = creatureRepository.save(creature);
        return creature;
    }
}
