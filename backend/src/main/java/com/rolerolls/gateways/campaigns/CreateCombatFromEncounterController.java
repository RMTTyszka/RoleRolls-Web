package com.rolerolls.gateways.campaigns;

import com.rolerolls.domain.combats.Combat;
import com.rolerolls.domain.combats.CombatRepository;
import com.rolerolls.domain.combats.services.encounters.CombatFromEncounterBuilder;
import com.rolerolls.domain.encounters.Encounter;
import com.rolerolls.domain.encounters.EncounterRepository;
import com.rolerolls.gateways.campaigns.dtos.CreateCombatFromEncounterInput;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/combat-from-encounter",  produces = "application/json; charset=UTF-8")
public class CreateCombatFromEncounterController  {


    @Autowired
    private CombatFromEncounterBuilder combatFromEncounterBuilder;
    @Autowired
    private EncounterRepository encounterRepository;
    @Autowired
    private CombatRepository combatRepository;


    @PostMapping("/{campaignId}")
    public @ResponseBody
    Combat create(@RequestParam UUID campaignId, @RequestBody CreateCombatFromEncounterInput input) {
        Encounter encounter = encounterRepository.findById(input.encounterId).get();

        Combat combat = combatFromEncounterBuilder.BuildFromEncounter(encounter);
        combat.setCampaignId(campaignId);
        combat = combatRepository.save(combat);
        return combat;
    }
}
