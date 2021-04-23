package com.rolerolls.domain.skills;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class SkillsService {

    @Autowired
    SkillRepository skillRepository;
    @Autowired
    CreatureSkillsRepository creatureSkillsRepository;

    public CreatureSkills save(CreatureSkills skills) {
        skills.setCombat(skillRepository.save(skills.getCombat()));
        skills.setKnowledge(skillRepository.save(skills.getKnowledge()));
        skills.setNimbleness(skillRepository.save(skills.getNimbleness()));
        skills.setPerception(skillRepository.save(skills.getPerception()));
        skills.setRelationship(skillRepository.save(skills.getRelationship()));
        skills.setResistance(skillRepository.save(skills.getResistance()));
        skills.setSports(skillRepository.save(skills.getSports()));
        return creatureSkillsRepository.save(skills);
    }

    public void addMajorSkillPoint(CreatureSkills creatureSkills, String skillName) {
        Skill skill = creatureSkills.getMajorSkill(skillName);
        boolean hasManjorSkillPoints = creatureSkills.getRemainingPoints() > 0;
        if (hasManjorSkillPoints) {
            if (skill.canAddMajorPoint()){
                skill.addPoint();
            }
        }
    }
    public void removeMajorSkillPoint(CreatureSkills creatureSkills, String skillName) {
        Skill skill = creatureSkills.getMajorSkill(skillName);
        if (skill.canRemoveMajorSkillPoint()) {
            skill.removePoint();
        }
    }

    public void addMinorSkillPoint(CreatureSkills creatureSkills, String skillName ) {
        Skill skill = creatureSkills.getSkillByMinorSkill(skillName);
        if (skill.hasRemainingMinorPoints()) {
            if (skill.canAddMinorPoint(skillName))
                skill.addPoint(skillName);
        }
    }
    public void removeMinorSkillPoint(CreatureSkills creatureSkills, String skillName ) {
        Skill skill = creatureSkills.getSkillByMinorSkill(skillName);
        Integer currentPoints = skill.getMinorSkillPoint(skillName);
        if (currentPoints > 0) {
                skill.removePoint(skillName);
        }
    }
}
