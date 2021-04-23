package com.rolerolls.domain.effects;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.Embeddable;

@Embeddable
public class EffectInstance {
    @Getter @Setter
    private EffectType effectType;

    @Getter @Setter
    private Integer duration;

    @Getter @Setter
    private Integer difficulty;
    @Getter @Setter
    private Integer complexity;
    @Getter @Setter
    private Integer level;


    public EffectInstance() {
    }
    public EffectInstance(EffectType effectType, Integer level, Integer duration, Integer difficulty, Integer complexity) {
        this.effectType = effectType;
        this.level = level;
        this.duration = duration;
        this.difficulty = difficulty;
        this.complexity = complexity;
    }

}
