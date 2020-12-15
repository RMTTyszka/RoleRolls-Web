package com.loh.application.skills.dtos;

import com.loh.domain.skills.Skill;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import java.util.Arrays;
import java.util.List;

public class PerceptionDto extends SkillDto {
    public PerceptionDto() {
        super();
    }
    public Integer perceive = 0;
    public Integer feeling = 0;
    public Integer search = 0;
}
