package com.rolerolls.domain.skills;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import java.util.Arrays;
import java.util.List;

@Entity
@DiscriminatorValue("Relationship")
public class Relationship extends Skill {
    public Relationship() {
        super();
    }
    @Override
    protected Integer getUsedMinorPoints() {
        return diplomacy + bluff + intimidate;
    }

    @Override
    public List<String> getList() {
        return Arrays.asList("diplomacy", "bluff", "intimidate");
    }
    @Getter @Setter
    private Integer diplomacy = 0;
    @Getter @Setter
    private Integer bluff = 0;
    @Getter @Setter
    private Integer intimidate = 0;
}
