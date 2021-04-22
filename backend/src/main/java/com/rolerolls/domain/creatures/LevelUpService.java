package com.rolerolls.domain.creatures;

import com.rolerolls.domain.items.instances.ItemInstanceRepository;
import com.rolerolls.domain.items.equipables.armors.instances.ArmorInstanceRepository;
import com.rolerolls.domain.items.equipables.weapons.instances.WeaponInstanceRepository;
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
