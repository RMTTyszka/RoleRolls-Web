package com.loh.combat;

import com.loh.creatures.Attributes;
import com.loh.creatures.Creature;
import com.loh.creatures.CreatureRepository;
import com.loh.creatures.heroes.Hero;
import com.loh.creatures.monsters.Monster;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Random;
import java.util.UUID;

@Service
public class CombatService {

    private Random random = new Random();
    @Autowired
    private AttackService attackService;

    @Autowired
    private CreatureRepository creatureRepository;
    @Autowired
    private CombatRepository combatRepository;

    public AttackDetails processFullAttack(Creature attacker, Creature target) {

        return attackService.fullAttack(attacker, target);
    }
    public AttackDetails processFullAttack(UUID attackerId, UUID targetId) {
        Creature attacker = creatureRepository.findById(attackerId).get();
        Creature target = creatureRepository.findById(targetId).get();
        AttackDetails attackDetails = attacker.fullAttack(target, attackService);

        target.takeDamage(attackDetails.getTotalDamage());

        creatureRepository.save(target);
        creatureRepository.save(attacker);

        return attackDetails;
    }

    public Combat startCombat(Combat combat) {
        return rollForInitiatives(combat);
    }

    public Integer rollForInitiative(Creature creature) {
        Integer roll = random.nextInt(20) + 1;
        return roll + creature.getAttributeLevel(Attributes.Agility);
    }
    public Combat rollForInitiatives(Combat combat) {
        for (Hero hero: combat.getHeroes()) {
            Initiative initiative = new Initiative(hero, rollForInitiative(hero));
            combat.addInitiative(initiative);
        }
        for (Monster monster: combat.getMonsters()) {
            Initiative initiative = new Initiative(monster, rollForInitiative(monster));
            combat.addInitiative(initiative);
        }
        return combatRepository.save(combat);
    }

    public Initiative endTurn(Combat combat, Creature creature) {
        creature = creature.processEndOfTurn(creatureRepository);
        Initiative nextInitiative = combat.endTurn(creature, combatRepository);
        return nextInitiative;
    }
    public Initiative endTurn(UUID combatId, UUID creatureId) {
        Creature creature = creatureRepository.findById(creatureId).get();
        Combat combat = combatRepository.findById(combatId).get();
        return endTurn(combat, creature);
    }
}
