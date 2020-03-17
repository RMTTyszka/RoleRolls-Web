package com.loh.creatures;

import com.loh.items.ItemInstanceRepository;
import com.loh.items.armors.armorInstance.ArmorInstanceRepository;
import com.loh.items.weapons.weaponInstance.WeaponInstanceRepository;
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

    public void levelUpForTest(UUID creatureId) {
        Creature creature = creatureRepository.findById(creatureId).get();

        levelUpForTest(creature);
    }

    public void levelUpForTest(Creature creature) {
        creature.levelUpforTest(creatureRepository, itemInstanceRepository);
    }
}
