package com.loh.combat;

import java.util.ArrayList;
import java.util.List;

public class AttackResult {
    public Integer hits;
    public Integer criticalHits;
    public Integer criticalMisses;
    public Integer totalDamage() {
        return damages.stream().reduce(0, (subtotal, damage) -> subtotal + damage);
    };
    public List<Integer> damages = new ArrayList<>();
}
