package com.loh.combat;

import com.loh.shared.BaseRepository;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface CombatRepository extends BaseRepository<Combat>, JpaSpecificationExecutor<Combat> {
    Page<Combat> getAllByCampaignIdAndHasStarted(UUID campaignId, boolean hasStarted, Pageable page);
}
