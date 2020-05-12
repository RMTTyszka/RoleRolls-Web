package com.loh.creatures.masterTools;

import com.loh.creatures.Creature;
import com.loh.creatures.CreatureRepository;
import com.loh.effects.EffectInstance;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class MasterCreatureService {

    @Autowired
    CreatureRepository creatureRepository;
    public Creature updateLife(UUID creatureId, Integer life) {
        Creature creature = creatureRepository.findById(creatureId).get();
        creature.setCurrentLife(life);
        creatureRepository.save(creature);
        return creature;
    }
    public Creature updateMoral(UUID creatureId, Integer value) {
        Creature creature = creatureRepository.findById(creatureId).get();
        creature.setCurrentMoral(value);
        creatureRepository.save(creature);
        return creature;
    }
    public Creature heal(UUID creatureId, Integer value) {
        Creature creature = creatureRepository.findById(creatureId).get();
        creature.heal(value);
        creatureRepository.save(creature);
        return creature;
    }
    public Creature removeEffect(UUID creatureId, EffectInstance effect) {
        Creature creature = creatureRepository.findById(creatureId).get();
        creature.removeEffect(effect);
        creatureRepository.save(creature);
        return creature;
    }
    public Creature updateEffect(UUID creatureId, EffectInstance effect) {
        Creature creature = creatureRepository.findById(creatureId).get();
        creature.updateEffect(effect);
        creatureRepository.save(creature);
        return creature;
    }
}
