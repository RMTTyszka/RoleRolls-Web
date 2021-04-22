package com.rolerolls.application.skills.dtos;

import com.rolerolls.shared.EntityDto;

import java.util.List;

public abstract class SkillDto extends EntityDto {
    public Integer level;
    public Integer points = 0;
    public Integer maxPointPerSkill;
    public Integer maxTotalMajorPoints;
    public Integer totalMinorPoints;
    public Integer remainingMinorPoints;
    public boolean removeMajorSkillPoint;
    public Integer getUsedMinorPoints;
    public List<String> list;
}
