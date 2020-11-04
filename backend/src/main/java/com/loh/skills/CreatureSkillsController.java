package com.loh.skills;

import com.loh.creatures.CreatureRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/skills",  produces = "application/json; charset=UTF-8")
public class CreatureSkillsController {
    @Autowired
    private CreatureRepository creatureRepository;
    @Autowired
    private SkillsService skillsService;

    @PostMapping(path="/creature/{creatureId}/addMajorSkillPoint")
    public @ResponseBody
    CreatureSkills addMajorSkillPoint(@PathVariable UUID creatureId, @RequestParam String skillName, @RequestBody CreatureSkills creatureSkills){
        skillsService.addMajorSkillPoint(creatureSkills, skillName);
        return creatureSkills;
    }
    @PostMapping(path="/creature/{creatureId}/removeMajorSkillPoint")
    public @ResponseBody
    CreatureSkills removeMajorSkillPoint(@PathVariable UUID creatureId, @RequestParam String skillName, @RequestBody CreatureSkills creatureSkills){
        skillsService.removeMajorSkillPoint(creatureSkills, skillName);
        return creatureSkills;
    }
    @PostMapping(path="/creature/{creatureId}/addMinorSkillPoint")
    public @ResponseBody
    CreatureSkills addMinorSkillPoint(@PathVariable UUID creatureId, @RequestParam String skillName, @RequestBody CreatureSkills creatureSkills){
        skillsService.addMinorSkillPoint(creatureSkills, skillName);
        return creatureSkills;
    }
    @PostMapping(path="/creature/{creatureId}/removeMinorSkillPoint")
    public @ResponseBody
    CreatureSkills removeMinorSkillPoint(@PathVariable UUID creatureId, @RequestParam String skillName, @RequestBody CreatureSkills creatureSkills){
        skillsService.removeMinorSkillPoint(creatureSkills, skillName);
        return creatureSkills;
    }
}
