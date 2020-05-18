package com.loh.masters;


import com.loh.creatures.Creature;
import com.loh.masters.dtos.AddOrRemoveBonusInput;
import com.loh.masters.dtos.RemoveEffectInput;
import com.loh.masters.dtos.TakeDamageInput;
import com.loh.masters.dtos.UpdateCreatureLifeAndMoralInput;
import lombok.SneakyThrows;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.io.UnsupportedEncodingException;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/masterTools",  produces = "application/json; charset=UTF-8")
public class MasterCreatureController {
    @Autowired
    private MasterCreatureService masterCreatureService;
    @SneakyThrows
    @PostMapping(path="/updateLife")
    public @ResponseBody
    Creature updateLife(@RequestBody UpdateCreatureLifeAndMoralInput input) {
        Creature creature = masterCreatureService.updateLife(input.creatureId, input.value, input.combatId);
        return creature;
    }
    @SneakyThrows
    @PostMapping(path="/updateMoral")
    public @ResponseBody
    Creature updateMoral(@RequestBody UpdateCreatureLifeAndMoralInput input) {
        Creature creature = masterCreatureService.updateMoral(input.creatureId, input.value, input.combatId);
        return creature;
    }
    @SneakyThrows
    @PostMapping(path="/heal")
    public @ResponseBody
    Creature heal(@RequestBody UpdateCreatureLifeAndMoralInput input) {
        Creature creature = masterCreatureService.heal(input.creatureId, input.value, input.combatId);
        return creature;
    }
    @PostMapping(path="/removeEffect")
    public @ResponseBody
    Creature removeEffect(@RequestBody RemoveEffectInput input) throws UnsupportedEncodingException {
        Creature creature = masterCreatureService.removeEffect(input.creatureId, input.effect, input.combatId);
        return creature;
    }
    @PostMapping(path="/updateEffect")
    public @ResponseBody
    Creature updateEffectLevel(@RequestBody RemoveEffectInput input) throws UnsupportedEncodingException {
        Creature creature = masterCreatureService.updateEffect(input.creatureId, input.effect, input.combatId);
        return creature;
    }
    @PostMapping(path="/takeDamage")
    public @ResponseBody
    Creature takeDamage(@RequestBody TakeDamageInput input) throws UnsupportedEncodingException {
        Creature creature = masterCreatureService.takeAttack(input);
        return creature;
    }
    @PostMapping(path="/addBonus")
    public @ResponseBody
    Creature addBonus(@RequestBody AddOrRemoveBonusInput input) throws UnsupportedEncodingException {
        Creature creature = masterCreatureService.addBonus(input.creatureId, input.bonus, input.combatId);
        return creature;
    }
    @PostMapping(path="/removeBonus")
    public @ResponseBody
    Creature removeBonus(@RequestBody AddOrRemoveBonusInput input) throws UnsupportedEncodingException {
        Creature creature = masterCreatureService.removeBonus(input.creatureId, input.bonus, input.combatId);
        return creature;
    }

}
