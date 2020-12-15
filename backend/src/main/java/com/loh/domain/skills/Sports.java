package com.loh.domain.skills;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import java.util.Arrays;
import java.util.List;

@Entity
@DiscriminatorValue("Sports")
public class Sports extends Skill {
    public Sports() {
        super();
    }
    @Override
    protected Integer getUsedMinorPoints() {
        return jump + climb + athleticism;
    }
    @Override
    public List<String> getList() {
        return Arrays.asList("jump", "climb", "athleticism");
    }

    @Getter @Setter
    private Integer jump = 0;
    @Getter @Setter
    private Integer climb = 0;
    @Getter @Setter
    private Integer athleticism = 0;
}
