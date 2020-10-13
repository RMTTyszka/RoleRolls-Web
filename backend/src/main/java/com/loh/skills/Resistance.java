package com.loh.skills;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;

@Entity
@DiscriminatorValue("Resistance")
public class Resistance extends Skill {
    public Resistance() {
        super();
    }
    @Override
    protected Integer getUsedPoints() {
        return mysticism + toughness + reflex;
    }
    @Getter @Setter
    private Integer mysticism = 0;
    @Getter @Setter
    private Integer toughness = 0;
    @Getter @Setter
    private Integer reflex = 0;
}
