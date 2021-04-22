package com.rolerolls.domain.effects;

import java.util.List;
import java.util.stream.Collectors;

public class EffectProcessor {

    
    public static List<EffectInstance> updateEffect(List<EffectInstance> effects, EffectInstance effect){
        EffectInstance existingEffect = effects.stream().filter(e -> e.getEffectType() == effect.getEffectType()).findFirst().orElse(null);
        if (existingEffect != null) {
            existingEffect = processEffectLevelUp(existingEffect, effect.getLevel());
            if (existingEffect.getLevel() <= 0) {
                effects = removeEffect(effects, effect);
            }
        } else {
            effects.add(effect);
        }
        return effects;

    }
    public static List<EffectInstance> removeEffect(List<EffectInstance> effects, EffectInstance effect){
        effects = effects.stream().filter(e -> e.getEffectType() != effect.getEffectType()).collect(Collectors.toList());
        return effects;
    }
    
    
    public static EffectInstance processEffectLevelUp(EffectInstance effectInstance, Integer levelDoAdd) {

        switch (effectInstance.getEffectType()){

            case Death:
                return processDeadLevelUp(effectInstance, levelDoAdd);
            case Unconscious:
                return processUnconsciousLevelUp(effectInstance, levelDoAdd);
            case Poison:
                return processPoisonLevelUp(effectInstance, levelDoAdd);
            case Burn:
                return processBurnLevelUp(effectInstance, levelDoAdd);
            case Slow:
                return processSlowLevelUp(effectInstance, levelDoAdd);
            default:
                throw new IllegalStateException("Unexpected value: " + effectInstance.getEffectType());
        }
    }


    private static EffectInstance processDeadLevelUp(EffectInstance effectInstance, Integer levelDoAdd) {
        effectInstance.setLevel(effectInstance.getLevel() + levelDoAdd);
        return effectInstance;
    }
    private static EffectInstance processUnconsciousLevelUp(EffectInstance effectInstance, Integer levelDoAdd) {
        effectInstance.setLevel(effectInstance.getLevel() + levelDoAdd);
        return effectInstance;
    }
    private static EffectInstance processPoisonLevelUp(EffectInstance effectInstance, Integer levelDoAdd) {
        effectInstance.setLevel(effectInstance.getLevel() + levelDoAdd);
        return effectInstance;
    }
    private static EffectInstance processBurnLevelUp(EffectInstance effectInstance, Integer levelDoAdd) {
        effectInstance.setLevel(effectInstance.getLevel() + levelDoAdd);
        return effectInstance;
    }
    private static EffectInstance processSlowLevelUp(EffectInstance effectInstance, Integer levelDoAdd) {
        effectInstance.setLevel(effectInstance.getLevel() + levelDoAdd);
        return effectInstance;
    }
}
