package com.loh.domain.campaigns.rolls;

import com.loh.shared.BaseRepository;

import java.util.ArrayList;
import java.util.UUID;

public interface CampaignRollHistoricRepository extends BaseRepository<CampaignRollHistoric> {
    ArrayList<CampaignRollHistoric> findAllByCampaignId(UUID campaignId);
}
