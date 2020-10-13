package com.loh.skills;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;

@Entity
@DiscriminatorValue("Perception")
public class Perception extends Skill {
    public Perception() {
        super();
    }
    @Override
    protected Integer getUsedPoints() {
        return perceive + feeling + search;
    }
    @Getter @Setter
    private Integer perceive = 0;
    @Getter @Setter
    private Integer feeling = 0;
    @Getter @Setter
    private Integer search = 0;
}
