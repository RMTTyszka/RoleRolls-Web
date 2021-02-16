package com.loh.application.combats.dtos;

import com.loh.domain.combats.DamageDetails;
import com.loh.rolls.Roll;
import lombok.Getter;

import java.util.List;

public class AttackResult {
    @Getter
    private Integer hits;
    @Getter
    private Integer criticalHits;
    @Getter
    private Integer criticalMisses;
    public Integer getTotalDamage() {
        return damageDetails.getDamages().stream().map(d -> d.getReducedDamage()).reduce(0, (subtotal, damage) -> subtotal + damage);
    }

    @Getter
    private DamageDetails damageDetails;
    @Getter
    private List<Roll> rolls;
    @Getter
    private Integer numberOfAttacks;
    @Getter
    private Integer hitBonus;

    public AttackResult(Integer hits, Integer criticalHits, Integer criticalMisses, DamageDetails damageDetails, List<Roll> rolls, Integer numberOfAttacks, Integer hitBonus) {
        this.hits = hits;
        this.criticalHits = criticalHits;
        this.criticalMisses = criticalMisses;
        this.damageDetails = damageDetails;
        this.rolls = rolls;
        this.numberOfAttacks = numberOfAttacks;
        this.hitBonus = hitBonus;
    }
}
