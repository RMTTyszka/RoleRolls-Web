package com.loh.skills;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;

@Entity
@DiscriminatorValue("Nimbleness")
public class Nimbleness extends Skill {
    public Nimbleness() {
        super();
    }
    @Override
    protected Integer getUsedPoints() {
        return steal + stealth + operateMechanisms;
    }
    @Getter @Setter
    private Integer steal = 0;
    @Getter @Setter
    private Integer stealth = 0;
    @Getter @Setter
    private Integer operateMechanisms = 0;
}
