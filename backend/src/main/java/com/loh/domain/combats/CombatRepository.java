package com.loh.domain.combats;

import com.loh.shared.LegacyBaseRepository;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

import java.util.UUID;

public interface CombatRepository extends LegacyBaseRepository<Combat>, JpaSpecificationExecutor<Combat> {
    Page<Combat> getAllByCampaignIdAndHasStarted(UUID campaignId, boolean hasStarted, Pageable page);
}
