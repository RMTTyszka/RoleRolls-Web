package com.loh.skills;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import java.util.Arrays;
import java.util.List;

@Entity
@DiscriminatorValue("Nimbleness")
public class Nimbleness extends Skill {
    public Nimbleness() {
        super();
    }
    @Override
    protected Integer getUsedMinorPoints() {
        return steal + stealth + operateMechanisms;
    }

    @Override
    public List<String> getList() {
        return Arrays.asList("steal", "stealth", "operateMechanisms");
    }

    @Getter @Setter
    private Integer steal = 0;
    @Getter @Setter
    private Integer stealth = 0;
    @Getter @Setter
    private Integer operateMechanisms = 0;
}
