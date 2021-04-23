package com.rolerolls.domain.skills;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import java.util.Arrays;
import java.util.List;

@Entity
@DiscriminatorValue("Perception")
public class Perception extends Skill {
    public Perception() {
        super();
    }
    @Override
    protected Integer getUsedMinorPoints() {
        return perceive + feeling + search;
    }

    @Override
    public List<String> getList() {
        return Arrays.asList("perceive", "feeling", "search");
    }
    @Getter @Setter
    private Integer perceive = 0;
    @Getter @Setter
    private Integer feeling = 0;
    @Getter @Setter
    private Integer search = 0;
}
