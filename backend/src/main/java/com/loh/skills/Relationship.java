package com.loh.skills;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;

@Entity
@DiscriminatorValue("Relationship")
public class Relationship extends Skill {
    public Relationship() {
        super();
    }
    @Override
    protected Integer getUsedPoints() {
        return diplomacy + bluff + intimidate;
    }
    @Getter @Setter
    private Integer diplomacy = 0;
    @Getter @Setter
    private Integer bluff = 0;
    @Getter @Setter
    private Integer intimidate = 0;
}
