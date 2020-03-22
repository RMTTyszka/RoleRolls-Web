package com.loh.combat;

import com.loh.creatures.Creature;
import com.loh.creatures.heroes.equipment.GripType;
import com.loh.rolls.DiceRoller;
import com.loh.rolls.TestResult;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
public class AttackService {

    @Autowired
    private DiceRoller roller;

    public AttackDetails fullAttack(Creature attacker, Creature target) {

        AttackResult mainWeaponAttackResult = mainWeaponAttack(attacker, target);
        AttackResult offWeaponAttackResult = null;
        if (attacker.getEquipment().getMainWeaponGripType() == GripType.TwoWeaponsLight || attacker.getEquipment().getMainWeaponGripType() == GripType.TwoWeaponsMedium) {
            offWeaponAttackResult = offWeaponAttack(attacker, target);
        }

        AttackDetails attackDetails = new AttackDetails(mainWeaponAttackResult, offWeaponAttackResult, target.getEvasion(), target.getDefense());
        return attackDetails;

    }

    private AttackResult offWeaponAttack(Creature attacker, Creature target) {
        Integer damageBonus = attacker.getOffWeaponAttributes().getDamageBonus();
        Integer weaponDamage = attacker.getOffWeaponAttributes().getDamage();
        Integer hitAttributePoints = attacker.getAttributePoints(attacker.getEquipment().getOffWeapon().getWeaponModel().getBaseWeapon().getHitAttribute());
        Integer hitBonus = attacker.getOffWeaponAttributes().getHitBonus() + attacker.getInateLevelBonus(hitAttributePoints);
        AttackResult attackResult = attack(
                weaponDamage,
                hitAttributePoints,
                hitBonus,
                damageBonus,
                target.getEvasion(),
                attacker.getOffWeaponAttributes().getAttackComplexity() + 1,
                target.getDodge(),
                target.getDefense()
        );

        return attackResult;
    }

    private AttackResult attack(Integer weaponDamage,Integer hitAttributePoints, Integer hitBonus, Integer damageBonus, Integer evasion, Integer complexity, Integer dodge, Integer defense) {
        TestResult attackTest = roller.makeTest(hitAttributePoints, hitBonus, evasion, complexity);
        List<Integer> damages = new ArrayList<>();

        for (int attackIndex = 0; attackIndex < attackTest.getSuccesses() - dodge; attackIndex++) {
            damages.add(getDamage(weaponDamage, damageBonus, defense));
        }
        AttackResult output = new AttackResult(
                attackTest.getSuccesses() - dodge,
                attackTest.getCriticalSuccesses(),
                attackTest.getCriticalFailures(),
                damages,
                attackTest.getRolls(),
                attackTest.getNumberOfRolls()
        );
        return output;
    }

    private AttackResult mainWeaponAttack(Creature attacker, Creature target) {
        Integer damageBonus = attacker.getMainWeaponAttributes().getDamageBonus();
        Integer weaponDamage = attacker.getMainWeaponAttributes().getDamage();
        Integer hitAttributePoints = attacker.getAttributePoints(attacker.getEquipment().getMainWeapon().getWeaponModel().getBaseWeapon().getHitAttribute());
        Integer hitBonus = attacker.getMainWeaponAttributes().getHitBonus() + attacker.getInateLevelBonus(hitAttributePoints);
        AttackResult attackResult = attack(
                weaponDamage,
                hitAttributePoints,
                hitBonus,
                damageBonus,
                target.getEvasion(),
                attacker.getMainWeaponAttributes().getAttackComplexity(),
                target.getDodge(),
                target.getDefense()
        );

        return attackResult;
    }
    private Integer getDamage(Integer weaponDamage, Integer damageBonus, Integer defense) {
        Integer damageRoll = roller.getRoll(weaponDamage);
         damageRoll = weaponDamage / 2;
        Integer damage = damageRoll + damageBonus - defense;
        return damage > 0 ? damage : 1;
    }
}
