package com.loh.domain.combats;

import lombok.Getter;

public class DamageResult {
    @Getter
    private Integer crudeDamage;
    @Getter
    private Integer reducedDamage;
    @Getter
    private Integer damageBonus;
    @Getter
    private Integer defense;

    public DamageResult(Integer crudeDamage, Integer reducedDamage, Integer damageBonus, Integer defense) {
        this.crudeDamage = crudeDamage;
        this.reducedDamage = reducedDamage;
        this.damageBonus = damageBonus;
        this.defense = defense;
    }
}
