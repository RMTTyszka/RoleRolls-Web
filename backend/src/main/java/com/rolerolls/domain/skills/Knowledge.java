package com.rolerolls.domain.skills;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import java.util.Arrays;
import java.util.List;

@Entity
@DiscriminatorValue("Knowledge")
public class Knowledge extends Skill {
    public Knowledge() {
        super();
    }

    @Override
    protected Integer getUsedMinorPoints() {
        return arcane + religion + nature;
    }
    @Override
    public List<String> getList() {
        return Arrays.asList("arcane", "religion", "nature");
    }
    @Getter @Setter
    private Integer arcane = 0;
    @Getter @Setter
    private Integer religion = 0;
    @Getter @Setter
    private Integer nature = 0;
}
