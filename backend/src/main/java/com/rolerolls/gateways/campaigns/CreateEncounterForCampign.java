package com.rolerolls.gateways.campaigns;

import com.rolerolls.domain.campaigns.Campaign;
import com.rolerolls.domain.campaigns.CampaignRepository;
import com.rolerolls.domain.campaigns.EncountersOnly;
import com.rolerolls.domain.encounters.Encounter;
import com.rolerolls.domain.encounters.EncounterRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.UUID;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/encounters-for-campaign",  produces = "application/json; charset=UTF-8")
public class CreateEncounterForCampign {


    @Autowired
    private EncounterRepository encounterRepository;

    @Autowired
    private CampaignRepository campaignRepository;


    @PostMapping("/{campaignId}")
    public @ResponseBody
    Campaign create(@RequestParam UUID campaignId, @RequestParam UUID encounterId) {
        Encounter encounter = encounterRepository.findById(encounterId).get();
        Campaign campaign = campaignRepository.findById(campaignId).get();
        campaign.addEncounter(encounter);
        campaign = campaignRepository.save(campaign);
        List<EncountersOnly> encounters = campaignRepository.findAllById(campaignId);
        return campaign;

    }
    @GetMapping("/{campaignId}")
    public @ResponseBody
    List<EncountersOnly> get(@RequestParam UUID campaignId) {
        List<EncountersOnly> encounters = campaignRepository.findAllById(campaignId);
        return encounters;

    }
    @DeleteMapping("/{campaignId}/{encounterId}")
    public @ResponseBody
    Campaign delete(@RequestParam UUID campaignId, @RequestParam UUID encounterId) {
        Campaign campaign = campaignRepository.findById(campaignId).get();
        campaign.removeEncounter(encounterId);
        campaign = campaignRepository.save(campaign);
        return campaign;
    }
}
