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

        AttackDetails attackDetails = new AttackDetails(mainWeaponAttackResult, offWeaponAttackResult, target.getStatus().getEvasion(), target.getStatus().getDefense());
        return attackDetails;

    }

    private AttackResult offWeaponAttack(Creature attacker, Creature target) {
        Integer damageBonus = attacker.getOffWeaponAttributes().getDamageBonus();
        Integer weaponDamage = attacker.getOffWeaponAttributes().getDamage();
        Integer hitAttributePoints = attacker.getAttributePoints(attacker.getEquipment().getOffWeapon().getWeaponModel().getBaseWeapon().getHitAttribute());
        Integer hitBonus = attacker.getOffWeaponAttributes().getHitBonus() + attacker.getInnateLevelBonus(hitAttributePoints);
        AttackResult attackResult = attack(
                weaponDamage,
                hitAttributePoints,
                hitBonus,
                damageBonus,
                target.getStatus().getEvasion(),
                attacker.getOffWeaponAttributes().getAttackComplexity() + 1,
                target.getStatus().getDodge(),
                target.getStatus().getDefense()
        );

        return attackResult;
    }

    private AttackResult attack(Integer weaponDamage,Integer hitAttributePoints, Integer hitBonus, Integer damageBonus, Integer evasion, Integer complexity, Integer dodge, Integer defense) {
        TestResult attackTest = roller.makeTest(hitAttributePoints, hitBonus, evasion, complexity);
        List<DamageResult> damageResults = new ArrayList<>();

        for (int attackIndex = 0; attackIndex < attackTest.getSuccesses() - dodge; attackIndex++) {
            DamageResult damageResult = getDamage(weaponDamage, damageBonus, defense);
            damageResults.add(damageResult);
        }
        DamageDetails damageDetails = new DamageDetails(damageResults, damageBonus);
        AttackResult output = new AttackResult(
                attackTest.getSuccesses() - dodge,
                attackTest.getCriticalSuccesses(),
                attackTest.getCriticalFailures(),
                damageDetails,
                attackTest.getRolls(),
                attackTest.getNumberOfRolls(),
                hitBonus
        );
        return output;
    }

    private AttackResult mainWeaponAttack(Creature attacker, Creature target) {
        Integer damageBonus = attacker.getMainWeaponAttributes().getDamageBonus();
        Integer weaponDamage = attacker.getMainWeaponAttributes().getDamage();
        Integer hitAttributePoints = attacker.getAttributePoints(attacker.getEquipment().getMainWeapon().getWeaponModel().getBaseWeapon().getHitAttribute());
        Integer hitBonus = attacker.getMainWeaponAttributes().getHitBonus() + attacker.getInnateLevelBonus(hitAttributePoints);
        AttackResult attackResult = attack(
                weaponDamage,
                hitAttributePoints,
                hitBonus,
                damageBonus,
                target.getStatus().getEvasion(),
                attacker.getMainWeaponAttributes().getAttackComplexity(),
                target.getStatus().getDodge(),
                target.getStatus().getDefense()
        );

        return attackResult;
    }
    private DamageResult getDamage(Integer weaponDamage, Integer damageBonus, Integer defense) {
        Integer damageRoll = roller.getRoll(weaponDamage);
         damageRoll = weaponDamage / 2;
        Integer crudDamage = damageRoll + damageBonus;
        Integer reducedDamage = crudDamage - defense;
        reducedDamage = Integer.max(reducedDamage, 1);
        return new DamageResult(crudDamage, reducedDamage, damageBonus, defense);
    }
}
