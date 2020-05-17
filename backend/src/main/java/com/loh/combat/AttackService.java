package com.loh.combat;

import com.loh.creatures.Creature;
import com.loh.creatures.equipment.GripType;
import com.loh.rolls.DiceRoller;
import com.loh.rolls.TestResult;
import com.loh.shared.DamageType;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
public class AttackService {


    public AttackDetails fullAttack(Creature attacker, Creature target) {

        AttackResult mainWeaponAttackResult = mainWeaponAttack(attacker, target);
        AttackResult offWeaponAttackResult = null;
        if (attacker.getEquipment().getMainWeaponGripType() == GripType.TwoWeaponsLight || attacker.getEquipment().getMainWeaponGripType() == GripType.TwoWeaponsMedium) {
            offWeaponAttackResult = offWeaponAttack(attacker, target);
        }

        AttackDetails attackDetails = new AttackDetails(mainWeaponAttackResult, offWeaponAttackResult, attacker, target);
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
                attacker.getOffWeaponAttributes().getAttackComplexity(),
                target
        );

        return attackResult;
    }

    private AttackResult attack(Integer weaponDamage,Integer hitAttributePoints, Integer hitBonus, Integer damageBonus, Integer complexity, Creature target) {
        DiceRoller roller = new DiceRoller();
        TestResult attackTest = roller.makeTest(hitAttributePoints, hitBonus, target.getStatus().getEvasion(), complexity);
        List<DamageResult> damageResults = new ArrayList<>();

        for (int attackIndex = 0; attackIndex < attackTest.getSuccesses() - target.getStatus().getDodge(); attackIndex++) {
            Integer damage = getDamage(weaponDamage, damageBonus);
            Integer reducedDamage = target.takeDamage(damage, DamageType.Physical);
            DamageResult damageResult = new DamageResult(damage, reducedDamage, damageBonus, target.getStatus().getDefense());
            damageResults.add(damageResult);
        }
        DamageDetails damageDetails = new DamageDetails(damageResults, damageBonus);
        AttackResult output = new AttackResult(
                attackTest.getSuccesses() - target.getStatus().getDodge(),
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
                attacker.getMainWeaponAttributes().getAttackComplexity(),
                target
        );

        return attackResult;
    }
    private Integer getDamage(Integer weaponDamage, Integer damageBonus) {
        DiceRoller roller = new DiceRoller();
        Integer damageRoll = roller.getRoll(weaponDamage);
         damageRoll = weaponDamage / 2;
        Integer crudDamage = damageRoll + damageBonus;
        return crudDamage;
    }
}
