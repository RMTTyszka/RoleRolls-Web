package com.loh.application.creatures.controllers;

import com.loh.application.creatures.dtos.CreatureRollResult;
import com.loh.domain.creatures.Creature;
import com.loh.domain.creatures.CreatureRepository;
import com.loh.rolls.DiceRoller;
import com.loh.rolls.RollTestResult;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@CrossOrigin
@Controller    // This means that this clas
// s is a Controller
@RequestMapping(path="/creatures-rolls",  produces = "application/json; charset=UTF-8")
public class CreatureRollsController {

    @Autowired
    public CreatureRepository creatureRepository;
    public DiceRoller roller = new DiceRoller();

    @GetMapping(path="/creatures/{creatureId}")
    public @ResponseBody
    CreatureRollResult roll(@PathVariable UUID creatureId, @RequestParam String property, @RequestParam(required = false) Integer difficulty, @RequestParam(required = false) Integer complexity) {
        Creature creature = creatureRepository.findById(creatureId).get();
        RollTestResult result;
        if (difficulty == null || complexity == null){
            result =  roller.roll(creature.getPropertyPoints(property), creature.getPropertyBonus(property));
        } else {
            result = roller.rollTest(creature.getPropertyPoints(property), creature.getPropertyBonus(property), difficulty, complexity);
        }
        return new CreatureRollResult(creatureId, creature.getName(), property, result.isSuccess(), result.getRolls(), result.getBonusDice(), result.getNumberOfRolls(), result.getRollSuccesses(), result.getSuccesses(), result.getCriticalSuccesses(), result.getCriticalFailures(), result.getDifficulty(), result.getComplexity());
    }

}
