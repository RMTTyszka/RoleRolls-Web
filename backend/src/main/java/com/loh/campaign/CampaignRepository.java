package com.loh.campaign;

import com.loh.shared.BaseRepository;

import java.util.List;
import java.util.UUID;

public interface CampaignRepository extends BaseRepository<Campaign> {
    List<Campaign> findAllByIdIn(List<UUID> ids);
}
