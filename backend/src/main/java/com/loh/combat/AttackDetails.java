package com.loh.combat;

import com.loh.creatures.Creature;
import lombok.Getter;

public class AttackDetails {

    @Getter
    private AttackResult mainWeaponAttackResult;
    @Getter
    private AttackResult offWeaponAttackResult;

    @Getter
    private Integer evasion;
    @Getter
    private Integer defense;

    @Getter
    private Creature attacker;
    @Getter
    private Creature target;





    public Integer getTotalDamage() {
        Integer mainWeaponDamage = mainWeaponAttackResult != null ? mainWeaponAttackResult.getTotalDamage() : 0;
        Integer offWeaponDamage = offWeaponAttackResult != null ? offWeaponAttackResult.getTotalDamage() : 0;
        return mainWeaponDamage + offWeaponDamage;
    }

    public AttackDetails(AttackResult mainWeaponAttackResult, AttackResult offWeaponAttackResult, Creature attacker, Creature target) {
        this.mainWeaponAttackResult = mainWeaponAttackResult;
        this.offWeaponAttackResult = offWeaponAttackResult;
        this.defense = target.getStatus().getDefense();
        this.evasion = target.getStatus().getEvasion();
        this.attacker = attacker;
        this.target = target;
    }
}
