package com.loh.creatures;

import com.loh.items.ItemInstanceRepository;
import com.loh.items.equipable.armors.armorInstance.ArmorInstanceRepository;
import com.loh.items.equipable.weapons.weaponInstance.WeaponInstanceRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class LevelUpService {

    @Autowired
    CreatureRepository creatureRepository;

    @Autowired
    ArmorInstanceRepository armorInstanceRepository;
    @Autowired
    WeaponInstanceRepository weaponInstanceRepository;
    @Autowired
    ItemInstanceRepository itemInstanceRepository;

    public void levelUpForTest(UUID creatureId, Integer level) {
        Creature creature = creatureRepository.findById(creatureId).get();

        levelUpForTest(creature, level);
    }

    public void levelUpForTest(Creature creature, Integer level) {
        creature.levelUpforTest(creatureRepository, itemInstanceRepository, level);
    }
}
