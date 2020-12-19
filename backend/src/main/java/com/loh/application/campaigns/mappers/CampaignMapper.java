package com.loh.application.campaigns.mappers;

import com.google.gson.Gson;
import com.loh.application.creatures.dtos.CreatureRollResult;
import com.loh.domain.campaigns.rolls.CampaignRollHistoric;
import com.loh.rolls.Roll;
import org.mapstruct.Mapper;
import org.mapstruct.Mapping;
import org.mapstruct.Named;

import java.util.List;

@Mapper(componentModel = "spring", uses = IterableNonInterableUtil.class)
public interface CampaignMapper {
    @Mapping(source = "rolls", target = "rolls", qualifiedByName = "listToString" )
    CampaignRollHistoric map(CreatureRollResult rollResult);

    @Named("listToString")
    public static String listToString(List<Roll> rolls) {
        Gson gson = new Gson();
        return gson.toJson(rolls);
    }
}

