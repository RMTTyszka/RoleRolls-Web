package com.loh.skills;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class SkillsService {

    @Autowired
    SkillRepository skillRepository;
    @Autowired
    CreatureSkillsRepository creatureSkillsRepository;

    public CreatureSkills Create(CreatureSkills skills) {
        skills.setCombat(skillRepository.save(skills.getCombat()));
        skills.setKnowledge(skillRepository.save(skills.getKnowledge()));
        skills.setNimbleness(skillRepository.save(skills.getNimbleness()));
        skills.setPerception(skillRepository.save(skills.getPerception()));
        skills.setRelationship(skillRepository.save(skills.getRelationship()));
        skills.setResistance(skillRepository.save(skills.getResistance()));
        skills.setSports(skillRepository.save(skills.getSports()));
        return creatureSkillsRepository.save(skills);
    }
}
