package com.loh.combat;

import lombok.Getter;

import java.util.ArrayList;
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
    private List<Integer> damages = new ArrayList<>();
    @Getter
    private List<Integer> rolls = new ArrayList<>();

    public AttackResult(Integer hits, Integer criticalHits, Integer criticalMisses, List<Integer> damages, List<Integer> rolls) {
        this.hits = hits;
        this.criticalHits = criticalHits;
        this.criticalMisses = criticalMisses;
        this.damages = damages;
        this.rolls = rolls;
    }
}
