package com.rolerolls.domain.creatures;

import com.rolerolls.domain.effects.EffectInstance;
import com.rolerolls.domain.effects.EffectType;

public class DeathProcessor {

    public static Integer getDeathDifficulty(Integer life) {
        return 10 + life;
    }
    public static EffectInstance getDeathEffect(Integer life) {
        return new EffectInstance(EffectType.Death, 2, null, DeathProcessor.getDeathDifficulty(life), 1);
    }
}
