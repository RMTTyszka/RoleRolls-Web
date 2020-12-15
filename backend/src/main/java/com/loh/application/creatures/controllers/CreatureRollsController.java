package com.loh.application.creatures.controllers;

import com.loh.application.creatures.dtos.RollOutput;
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

    @GetMapping(path="creatures/{creatureId}")
    public @ResponseBody
    RollOutput roll(@PathVariable UUID creatureId, @RequestParam String property, @RequestParam Integer difficulty, @RequestParam Integer complexity) {
        Creature creature = creatureRepository.findById(creatureId).get();
        RollTestResult result = roller.rollTest(creature.getPropertyPoints(property), creature.getPropertyBonus(property), difficulty, complexity);
        return new RollOutput();
    }

}
