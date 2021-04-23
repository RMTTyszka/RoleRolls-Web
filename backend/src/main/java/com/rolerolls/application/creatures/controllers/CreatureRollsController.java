package com.rolerolls.application.creatures.controllers;

import com.rolerolls.application.creatures.dtos.CreatureRollResult;
import com.rolerolls.domain.creatures.Creature;
import com.rolerolls.domain.creatures.CreatureRepository;
import com.rolerolls.rolls.DcResult;
import com.rolerolls.rolls.DiceRoller;
import com.rolerolls.rolls.RollDCService;
import com.rolerolls.rolls.RollTestResult;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.UUID;

@CrossOrigin
@Controller    // This means that this clas
// s is a Controller
@RequestMapping(path="/creatures-rolls",  produces = "application/json; charset=UTF-8")
public class CreatureRollsController {

    @Autowired
    public CreatureRepository creatureRepository;
    @Autowired
    private RollDCService rollDCService;
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
    @GetMapping(path="/creatures/{creatureId}/chances")
    public @ResponseBody
    List<DcResult> chance(@PathVariable UUID creatureId, @RequestParam String property, @RequestParam Double requiredChance) {
        Creature creature = creatureRepository.findById(creatureId).get();
        return rollDCService.getDC(creature.getAttributePoints(property), creature.getPropertyBonus(property), requiredChance);
    }

}
