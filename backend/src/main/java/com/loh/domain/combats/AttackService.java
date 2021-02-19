package com.loh.domain.combats;

import com.loh.application.combats.dtos.AttackResult;
import com.loh.domain.creatures.Creature;
import com.loh.domain.creatures.equipments.GripType;
import com.loh.rolls.DiceRoller;
import com.loh.rolls.RollTestResult;
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
                attacker.getOffWeaponAttributes().getAttackComplexity() + 1,
                target
        );

        return attackResult;
    }

    private AttackResult attack(Integer weaponDamage,Integer hitAttributePoints, Integer hitBonus, Integer damageBonus, Integer complexity, Creature target) {
        DiceRoller roller = new DiceRoller();
       // System.out.println(target.getInnateLevelBonus(hitAttributePoints) + "x " + target.getEvasionInnateBonus());
       // System.out.println(hitBonus + "x " + target.getStatus().getEvasion());
        RollTestResult attackTest = roller.rollTest(hitAttributePoints, hitBonus, target.getStatus().getEvasion(), complexity);
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

    private Integer getDamage(Integer weaponDamage, Integer damageBonus) {
        DiceRoller roller = new DiceRoller();
        Integer damageRoll = roller.getRoll(weaponDamage);
         // damageRoll = weaponDamage / 2;
        Integer crudDamage = damageRoll + damageBonus;
        return crudDamage;
    }
}
