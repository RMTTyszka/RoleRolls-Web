package com.loh.application.campaigns;

import com.loh.application.campaigns.dtos.HeroNotFromAddedPlayerException;
import com.loh.application.campaigns.mappers.CampaignMapper;
import com.loh.application.creatures.dtos.CreatureRollResult;
import com.loh.domain.campaigns.rolls.CampaignRollHistoric;
import com.loh.domain.campaigns.rolls.CampaignRollHistoricRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

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

    @GetMapping(path = "/{campaignId}/rolls")
    public @ResponseBody List<CreatureRollResult> list(@PathVariable UUID campaignId) {
        List<CampaignRollHistoric> list =  campaignRollHistoricRepository.findAll(Specification.where(byCampaignId(campaignId)).and(orderByDate()));
        List<CreatureRollResult> result = list.stream().map(e -> campaignMapper.map(e)).collect(Collectors.toList());
        return result;
    }

    private Specification<CampaignRollHistoric> byCampaignId(UUID campaignId) {
        return (rolls, cq, cb) -> cb.equal(rolls.get("campaignId"), campaignId);
    }

    static Specification<CampaignRollHistoric> orderByDate() {
        return (root, criteriaQuery, criteriaBuilder) -> {
            criteriaQuery.orderBy(criteriaBuilder.asc(root.get("creationTime")));
            return criteriaBuilder.isNotNull(root);
        };
    }
}
