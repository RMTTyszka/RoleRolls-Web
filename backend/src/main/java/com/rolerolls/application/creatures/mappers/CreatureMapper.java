package com.rolerolls.application.creatures.mappers;

import com.rolerolls.application.creatures.dtos.CreatureDto;
import com.rolerolls.application.skills.dtos.*;
import com.rolerolls.domain.creatures.Creature;
import com.rolerolls.domain.skills.*;
import org.mapstruct.Mapper;
import org.mapstruct.Mapping;

@Mapper(componentModel = "spring")
public interface CreatureMapper {
    CreatureDto map(Creature dto);
    Creature map(CreatureDto creature);

    @Mapping(target="skillsList", ignore = true)
    CreatureSkills map(CreatureSkillsDto dto);
    CreatureSkillsDto map(CreatureSkills skill);

    @Mapping(target="list", ignore = true)
    CombatSkill map(CombatSkillDto dto);
    CombatSkillDto map(CombatSkill skill);

    @Mapping(target="list", ignore = true)
    Knowledge map(KnowledgeDto dto);
    KnowledgeDto map(Knowledge skill);

    @Mapping(target="list", ignore = true)
    Nimbleness map(NimblenessDto dto);
    NimblenessDto map(Nimbleness skill);

    @Mapping(target="list", ignore = true)
    Perception map(PerceptionDto dto);
    PerceptionDto map(Perception skill);

    @Mapping(target="list", ignore = true)
    Relationship map(RelationshipDto dto);
    RelationshipDto map(Relationship skill);

    @Mapping(target="list", ignore = true)
    Resistance map(ResistanceDto dto);
    ResistanceDto map(Resistance skill);

    @Mapping(target="list", ignore = true)
    Sports map(SportsDto dto);
    SportsDto map(Sports skill);
}
