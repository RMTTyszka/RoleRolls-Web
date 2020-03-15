package com.loh.combat;

import com.loh.creatures.Creature;
import com.loh.creatures.heroes.equipment.GripType;
import com.loh.items.weapons.weaponInstance.WeaponInstance;
import com.loh.rolls.DiceRoller;
import com.loh.rolls.RollResult;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class AttackService {

    @Autowired
    private DiceRoller roller;

    public AttackDetails fullAttack(Creature attacker, Creature target) {
        AttackDetails attackOutput =  new AttackDetails();

        return attackOutput;

    }

    private AttackResult attack(WeaponInstance weapon,Integer numberOfAttacks, Integer hitBonus, Integer damageBonus, Integer evasion, GripType gripType, Integer defense) {
        AttackResult output = new AttackResult();
        RollResult attackTest = roller.makeTest(numberOfAttacks, hitBonus, evasion, gripType.getAttackComplexity());

        return output;
    }
}
