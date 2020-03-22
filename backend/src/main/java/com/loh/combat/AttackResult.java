package com.loh.combat;

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
        return damages.stream().reduce(0, (subtotal, damage) -> subtotal + damage);
    };
    @Getter
    private List<Integer> damages;
    @Getter
    private List<Integer> rolls;
    @Getter
    private Integer numberOfAttacks;

    public AttackResult(Integer hits, Integer criticalHits, Integer criticalMisses, List<Integer> damages, List<Integer> rolls, Integer numberOfAttacks) {
        this.hits = hits;
        this.criticalHits = criticalHits;
        this.criticalMisses = criticalMisses;
        this.damages = damages;
        this.rolls = rolls;
        this.numberOfAttacks = numberOfAttacks;
    }
}
