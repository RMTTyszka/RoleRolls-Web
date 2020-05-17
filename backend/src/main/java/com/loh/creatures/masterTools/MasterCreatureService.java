package com.loh.creatures.masterTools;

import com.loh.combat.Combat;
import com.loh.combat.CombatRepository;
import com.loh.creatures.Creature;
import com.loh.creatures.CreatureRepository;
import com.loh.effects.EffectInstance;
import com.loh.shared.DamageType;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.io.UnsupportedEncodingException;
import java.util.UUID;

@Service
public class MasterCreatureService {

    @Autowired
    CombatRepository combatRepository;
    @Autowired
    CreatureRepository creatureRepository;
    public Creature updateLife(UUID creatureId, Integer life, UUID combatId) throws UnsupportedEncodingException {
        Creature creature = creatureRepository.findById(creatureId).get();
        creature.setCurrentLife(life);
        creatureRepository.save(creature);
        if (combatId != null) {
            Combat combat = combatRepository.findById(combatId).get();
            combat.addLog(String.format("Master set %s life to %d", creature.getName(), life));
            combatRepository.save(combat);
        }
        return creature;
    }
    public Creature updateMoral(UUID creatureId, Integer value, UUID combatId) throws UnsupportedEncodingException {
        Creature creature = creatureRepository.findById(creatureId).get();
        creature.setCurrentMoral(value);
        creatureRepository.save(creature);
        if (combatId != null) {
            Combat combat = combatRepository.findById(combatId).get();
            combat.addLog(String.format("Master set %s moral to %d", creature.getName(), value));
            combatRepository.save(combat);
        }  return creature;
    }
    public Creature heal(UUID creatureId, Integer value, UUID combatId) throws UnsupportedEncodingException {
        Creature creature = creatureRepository.findById(creatureId).get();
        creature.heal(value);
        creatureRepository.save(creature);
        if (combatId != null) {
            Combat combat = combatRepository.findById(combatId).get();
            combat.addLog(String.format("Master heal %s by %d", creature.getName(), value));
            combatRepository.save(combat);
        } return creature;
    }
    public Creature removeEffect(UUID creatureId, EffectInstance effect, UUID combatId) throws UnsupportedEncodingException {
        Creature creature = creatureRepository.findById(creatureId).get();
        creature.removeEffect(effect);
        creatureRepository.save(creature);
        if (combatId != null) {
            Combat combat = combatRepository.findById(combatId).get();
            combat.addLog(String.format("Master removed %s from %s", effect.getEffectType().toString(), creature.getName()));
            combatRepository.save(combat);
        } return creature;
    }
    public Creature updateEffect(UUID creatureId, EffectInstance effect, UUID combatId) throws UnsupportedEncodingException {
        Creature creature = creatureRepository.findById(creatureId).get();
        creature.updateEffect(effect);
        creatureRepository.save(creature);
        if (combatId != null) {
            Combat combat = combatRepository.findById(combatId).get();
            combat.addLog(String.format("Master altered %s effect from %s", effect.getEffectType().toString(), creature.getName()));
            combatRepository.save(combat);
        } return creature;
    }
    public Creature takeAttack(TakeDamageInput input) throws UnsupportedEncodingException {
        Creature creature = creatureRepository.findById(input.creatureId).get();
        Integer totalDamage = 0;
        for (int damage : input.physicalDamages) {
            totalDamage += creature.takeDamage(damage, DamageType.Physical);
        }
        for (int damage : input.arcaneDamages) {
            totalDamage += creature.takeDamage(damage, DamageType.Arcane);
        }
        for (int damage : input.fireDamages) {
            totalDamage += creature.takeDamage(damage, DamageType.Fire);
        }
        for (int damage : input.iceDamages) {
            totalDamage += creature.takeDamage(damage, DamageType.Ice);
        }
        for (int damage : input.lightningDamages) {
            totalDamage += creature.takeDamage(damage, DamageType.Lightning);
        }
        for (int damage : input.poisonDamages) {
            totalDamage += creature.takeDamage(damage, DamageType.Poison);
        }
        for (int damage : input.holyDamages) {
            totalDamage += creature.takeDamage(damage, DamageType.Holy);
        }
        for (int damage : input.necroticDamages) {
            totalDamage += creature.takeDamage(damage, DamageType.Necrotic);
        }
        for (int damage : input.sonicDamages) {
            totalDamage += creature.takeDamage(damage, DamageType.Sonic);
        }
        creatureRepository.save(creature);
        if (input.combatId != null) {
            Combat combat = combatRepository.findById(input.combatId).get();
            combat.addLog(String.format("%s took %d total damage", creature.getName(), totalDamage));
            combatRepository.save(combat);
        } return creature;
    }

}
