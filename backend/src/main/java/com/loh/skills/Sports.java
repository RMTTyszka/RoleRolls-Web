package com.loh.skills;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;

@Entity
@DiscriminatorValue("Sports")
public class Sports extends Skill {
    public Sports() {
        super();
    }
    @Override
    protected Integer getUsedPoints() {
        return jump + climb + athleticism;
    }
    @Getter @Setter
    private Integer jump = 0;
    @Getter @Setter
    private Integer climb = 0;
    @Getter @Setter
    private Integer athleticism = 0;
}
