package com.loh.application.campaigns;

import com.loh.application.campaigns.dtos.HeroNotFromAddedPlayerException;
import com.loh.application.campaigns.mappers.CampaignMapper;
import com.loh.application.creatures.dtos.CreatureRollResult;
import com.loh.domain.campaigns.rolls.CampaignRollHistoric;
import com.loh.domain.campaigns.rolls.CampaignRollHistoricRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.UUID;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/campaigns",  produces = "application/json; charset=UTF-8")
public class CampaignsRollsController {
    @Autowired
    CampaignMapper campaignMapper;
    @Autowired
    CampaignRollHistoricRepository campaignRollHistoricRepository;

    @PostMapping(path = "/{campaignId}/rolls")
    public @ResponseStatus(HttpStatus.OK) void create(@PathVariable UUID campaignId, @RequestBody CreatureRollResult rollResult) throws HeroNotFromAddedPlayerException {
        CampaignRollHistoric historic = campaignMapper.map(rollResult);
        historic.setCampaignId(campaignId);
        campaignRollHistoricRepository.save(historic);
    }

    @GetMapping(path = "/{campaignId}/rolls2/list")
    public ArrayList<CampaignRollHistoric> list(@PathVariable UUID campaignId) {
        ArrayList<CampaignRollHistoric> list =  campaignRollHistoricRepository.findAllByCampaignId(campaignId);
        return list;
    }
}
