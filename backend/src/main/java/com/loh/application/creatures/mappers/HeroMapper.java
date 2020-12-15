package com.loh.application.creatures.mappers;

import com.loh.application.creatures.heroes.dtos.HeroDto;
import com.loh.application.skills.dtos.*;
import com.loh.domain.creatures.heroes.Hero;
import com.loh.domain.skills.*;
import org.mapstruct.Mapper;
import org.mapstruct.Mapping;

@Mapper(componentModel = "spring")
public interface HeroMapper {
    HeroDto map(Hero heroDto);
    Hero map(HeroDto hero);

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
