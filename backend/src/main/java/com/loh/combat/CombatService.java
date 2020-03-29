package com.loh.combat;

import com.loh.creatures.Creature;
import com.loh.creatures.CreatureRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class CombatService {


    @Autowired
    private AttackService attackService;

    @Autowired
    private CreatureRepository creatureRepository;

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
}
