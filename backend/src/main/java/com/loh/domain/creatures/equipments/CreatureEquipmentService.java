package com.loh.domain.creatures.equipments;

import com.loh.domain.creatures.Creature;
import com.loh.domain.items.equipables.armors.instances.ArmorInstance;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class CreatureEquipmentService {

    public Creature equipArmor(Creature creature, UUID armorInstanceId) {
        ArmorInstance armor = (ArmorInstance) creature.getInventory().getItem(armorInstanceId);
        creature.equipArmor(armor);
        return creature;
    }
}
