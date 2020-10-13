package com.loh.skills;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;

@Entity
@DiscriminatorValue("Knowledge")
public class Knowledge extends Skill {
    public Knowledge() {
        super();
    }

    @Override
    protected Integer getUsedPoints() {
        return arcane + religion + nature;
    }

    @Getter @Setter
    private Integer arcane = 0;
    @Getter @Setter
    private Integer religion = 0;
    @Getter @Setter
    private Integer nature = 0;
}
