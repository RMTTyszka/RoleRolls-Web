package com.loh.campaign;

import com.loh.shared.LegacyBaseRepository;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.domain.Specification;

import java.util.List;
import java.util.UUID;

public interface CampaignRepository extends LegacyBaseRepository<Campaign> {
    List<Campaign> findAllByIdIn(List<UUID> ids);
    List<Campaign> findAllByNameIgnoreCaseContaining(String filter, Specification<Campaign> spec, Pageable page);
}
