package com.loh.combat;

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



    public Integer getTotalDamage() {
        Integer mainWeaponDamage = mainWeaponAttackResult != null ? mainWeaponAttackResult.getTotalDamage() : 0;
        Integer offWeaponDamage = offWeaponAttackResult != null ? offWeaponAttackResult.getTotalDamage() : 0;
        return mainWeaponDamage + offWeaponDamage;
    }

    public AttackDetails(AttackResult mainWeaponAttackResult, AttackResult offWeaponAttackResult, Integer evasion, Integer defense) {
        this.mainWeaponAttackResult = mainWeaponAttackResult;
        this.offWeaponAttackResult = offWeaponAttackResult;
        this.defense = defense;
        this.evasion = evasion;
    }
}
