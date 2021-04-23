package com.rolerolls.application.rolls.controllers;

import com.rolerolls.rolls.DcResult;
import com.rolerolls.rolls.RollDCService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/rolls",  produces = "application/json; charset=UTF-8")
public class RollDCController {
    @Autowired
    private RollDCService rollDCService;

    @GetMapping(path="/chances")
    public @ResponseBody
    List<DcResult> get(@RequestParam Integer points, @RequestParam Integer bonus, @RequestParam Double requiredChance) {
        return rollDCService.getDC(points, bonus, requiredChance);
    }
}
