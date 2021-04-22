package com.rolerolls.domain.campaigns;

import com.rolerolls.shared.BaseRepository;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.domain.Specification;

import java.util.List;
import java.util.UUID;

public interface CampaignRepository extends BaseRepository<Campaign> {
    List<Campaign> findAllByIdIn(List<UUID> ids);
    List<Campaign> findAllByNameIgnoreCaseContaining(String filter, Specification<Campaign> spec, Pageable page);
}
