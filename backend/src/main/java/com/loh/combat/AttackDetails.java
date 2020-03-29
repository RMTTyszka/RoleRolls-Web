package com.loh.combat;

import lombok.Getter;

import java.util.List;

public class AttackDetails {
    @Getter
    private Integer mainWeaponNumberOfAttacks;
    @Getter
    private Integer mainWeaponHits;
    @Getter
    private Integer mainWeaponCriticalHits;
    @Getter
    private Integer mainWeaponCriticalMisses;
    @Getter
    private Integer offWeaponNumberOfAttacks;
    @Getter
    private Integer offWeaponCriticalHits;
    @Getter
    private Integer offWeaponCriticalMisses;
    @Getter
    private Integer offWeaponHits;
    @Getter
    private List<Integer> mainWeaponRolls;
    @Getter
    private List<Integer> offWeaponRolls;
    @Getter
    private List<Integer> mainWeaponDamages;
    @Getter
    private List<Integer> offWeaponDamages;
    @Getter
    private Integer evasion;
    @Getter
    private Integer defense;
    @Getter
    private Integer mainWeaponHitBonus;
    @Getter
    private Integer offWeaponHitBonus;



    public Integer getTotalDamage() {
        Integer mainWeaponDamage = mainWeaponDamages != null ? mainWeaponDamages.stream().reduce(0, (a, b) -> a + b).intValue() : 0;
        Integer offWeaponDamage = offWeaponDamages != null ? offWeaponDamages.stream().reduce(0, (a, b) -> a + b).intValue() : 0;
        return mainWeaponDamage + offWeaponDamage;
    }

    public AttackDetails(AttackResult mainWeaponAttackResult, AttackResult offWeaponAttackResult, Integer evasion, Integer defense) {
        mainWeaponHits = mainWeaponAttackResult.getHits();
        mainWeaponCriticalHits = mainWeaponAttackResult.getCriticalHits();
        mainWeaponCriticalMisses = mainWeaponAttackResult.getCriticalMisses();
        mainWeaponRolls = mainWeaponAttackResult.getRolls();
        mainWeaponDamages = mainWeaponAttackResult.getDamages();
        mainWeaponNumberOfAttacks = mainWeaponAttackResult.getNumberOfAttacks();
        this.mainWeaponHitBonus = mainWeaponAttackResult.getHitBonus();

        if (offWeaponAttackResult != null) {
            offWeaponHits = offWeaponAttackResult.getHits();
            offWeaponCriticalHits = offWeaponAttackResult.getCriticalHits();
            offWeaponCriticalMisses = offWeaponAttackResult.getCriticalMisses();
            offWeaponRolls = offWeaponAttackResult.getRolls();
            offWeaponDamages = offWeaponAttackResult.getDamages();
            offWeaponNumberOfAttacks = offWeaponAttackResult.getNumberOfAttacks();
            this.offWeaponHitBonus = offWeaponAttackResult.getHitBonus();
        }

        this.defense = defense;
        this.evasion = evasion;
    }
}
