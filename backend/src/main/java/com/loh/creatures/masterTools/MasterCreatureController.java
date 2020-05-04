package com.loh.creatures.masterTools;


import com.loh.creatures.Creature;
import com.loh.creatures.CreatureRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/masterTools",  produces = "application/json; charset=UTF-8")
public class MasterCreatureController {
    @Autowired
    private CreatureRepository creatureRepository;
    @Autowired
    private MasterCreatureService masterCreatureService;
    @PostMapping(path="/updateLife")
    public @ResponseBody
    Creature updateLife(@RequestBody UpdateCreatureLifeInput input) {
        Creature creature = masterCreatureService.updateLife(input.creatureId, input.life);
        return creature;
    }
    @PostMapping(path="/removeEffect")
    public @ResponseBody
    Creature removeEffect(@RequestBody RemoveEffectInput input) {
        Creature creature = masterCreatureService.removeEffect(input.creatureId, input.effect);
        return creature;
    }
    @PostMapping(path="/updateEffect")
    public @ResponseBody
    Creature updateEffectLevel(@RequestBody RemoveEffectInput input) {
        Creature creature = masterCreatureService.updateEffect(input.creatureId, input.effect);
        return creature;
    }

}
