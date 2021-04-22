package com.rolerolls.application.skills.dtos;

import com.rolerolls.shared.EntityDto;

import java.util.List;

public class CreatureSkillsDto extends EntityDto {
    public SportsDto sports;
    public NimblenessDto nimbleness;
    public KnowledgeDto knowledge;
    public CombatSkillDto combat;
    public PerceptionDto perception;
    public ResistanceDto resistance;
    public RelationshipDto relationship;
    public Integer level;
    public Integer maxPoints;
    public Integer remainingPoints;
    public List<String> skillsList;
}
