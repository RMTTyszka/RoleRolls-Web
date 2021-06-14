package com.rolerolls.domain.combats;

import com.rolerolls.application.combats.dtos.CombatActionDto;
import com.rolerolls.domain.creatures.Attributes;
import com.rolerolls.domain.creatures.Creature;
import com.rolerolls.domain.creatures.heroes.Hero;
import com.rolerolls.domain.creatures.monsters.Monster;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.io.IOException;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.time.format.DateTimeFormatter;
import java.util.Random;
import java.util.UUID;

@Service
public class CombatService {

    private Random random = new Random();
    @Autowired
    private AttackService attackService;

    @Autowired
    private CombatRepository combatRepository;

    public AttackDetails processFullAttack(Combat combat, Creature attacker, Creature target) {
        return attackService.fullAttack(attacker, target);
    }
    public CombatActionDto processFullAttack(UUID combatId, UUID attackerId, UUID targetId) throws IOException, ClassNotFoundException {
        Combat combat = combatRepository.findById(combatId).get();
        Creature attacker = combat.findCreatureById(attackerId);
        Creature target = combat.findCreatureById(targetId);
        AttackDetails attackDetails = attacker.fullAttack(target, attackService);
        DateTimeFormatter dateFormat = DateTimeFormatter.ofPattern("dd-MM HH:mm:ss");
        LocalDateTime date = LocalDateTime.now(ZoneId.of("GMT-03:00"));
        combat.addLog(String.format("%s - %s attacked %s and caused %d damage", dateFormat.format(date), attacker.getName(), target.getName(), attackDetails.getTotalDamage()));
        combat = combatRepository.save(combat);
        return new CombatActionDto(combat, attackDetails, 0, 0, 0);
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

    public Combat endTurn(Combat combat, Creature creature) {
        combat.endTurn(creature, combatRepository);
        return combat;
    }
    public Combat endTurn(UUID combatId, UUID creatureId) {
        Combat combat = combatRepository.findById(combatId).get();
        Creature creature = combat.findCreatureById(creatureId);
        return endTurn(combat, creature);
    }
}
