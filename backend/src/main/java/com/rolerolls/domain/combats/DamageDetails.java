package com.rolerolls.domain.combats;

import lombok.Getter;

import java.util.List;

public class DamageDetails {
    @Getter
    private List<DamageResult> damages;

    @Getter
    private Integer damageBonus;

    public DamageDetails(List<DamageResult> damages, Integer damageBonus) {
        this.damages = damages;
        this.damageBonus = damageBonus;
    }
}
