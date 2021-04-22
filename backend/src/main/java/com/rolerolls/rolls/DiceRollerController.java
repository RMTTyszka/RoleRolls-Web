package com.rolerolls.rolls;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/roll",  produces = "application/json; charset=UTF-8")
public class DiceRollerController {
    DiceRoller diceRoller = new DiceRoller();

    @GetMapping(path="/makeTest")
    public @ResponseBody
    RollTestResult makeRoll(@RequestParam Integer level, @RequestParam Integer bonus, @RequestParam Integer difficulty, @RequestParam Integer complexity) {

        // This returns a JSON or XML with the users
        return diceRoller.rollTest(level, bonus, difficulty, complexity);
    }
}
