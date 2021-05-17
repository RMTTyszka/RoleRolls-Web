package com.rolerolls.domain.creatures.monsters.services;

import com.rolerolls.domain.creatures.monsters.Monster;
import com.rolerolls.domain.creatures.monsters.models.MonsterModel;
import com.rolerolls.domain.creatures.monsters.models.MonsterTemplateSkill;
import com.rolerolls.domain.skills.CreatureSkills;
import com.rolerolls.domain.skills.SkillsService;
import org.springframework.beans.factory.annotation.Autowired;

public class MonsterInstantiatorService {
    @Autowired
    private SkillsService skillsService;

    public Monster Instantiate(MonsterModel monsterModel, Integer level) {
        Monster monster = new Monster();
        monster.setMonsterModelId(monsterModel.getId());
        monster.setRace(monsterModel.getRace());
        monster.setRole(monsterModel.getRole());
        // TODO get random name from race
        monster.setName(monsterModel.getName());
        CreatureSkills creatureSkills = instantiateCreatureSkills(monsterModel);
        monster.setSkills(creatureSkills);
        monster.setBaseAttributes(monsterModel.getAttributes());


        return monster;
    }



    private CreatureSkills instantiateCreatureSkills(MonsterModel monsterModel) {
        CreatureSkills creatureSkills = new CreatureSkills();
        for (MonsterTemplateSkill skill : monsterModel.getSkills()) {
            skillsService.addMajorSkillPoint(creatureSkills, skill.getMajorSkill());

            skillsService.addMinorSkillPoint(creatureSkills, skill.getMinorSkill1());
            skillsService.addMinorSkillPoint(creatureSkills, skill.getMinorSkill1());
            skillsService.addMinorSkillPoint(creatureSkills, skill.getMinorSkill1());

            skillsService.addMinorSkillPoint(creatureSkills, skill.getMinorSkill2());
            skillsService.addMinorSkillPoint(creatureSkills, skill.getMinorSkill2());

            skillsService.addMinorSkillPoint(creatureSkills, skill.getMinorSkill3());
        }
        return creatureSkills;
    }
}
