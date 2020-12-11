package com.loh.skills;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import java.util.Arrays;
import java.util.List;

@Entity
@DiscriminatorValue("Resistance")
public class Resistance extends Skill {
    public Resistance() {
        super();
    }
    @Override
    protected Integer getUsedMinorPoints() {
        return mysticism + toughness + reflex;
    }
    @Override
    public List<String> getList() {
        return Arrays.asList("mysticism", "toughness", "reflex");
    }
    @Getter @Setter
    private Integer mysticism = 0;
    @Getter @Setter
    private Integer toughness = 0;
    @Getter @Setter
    private Integer reflex = 0;
}
