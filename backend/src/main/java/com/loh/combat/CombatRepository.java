package com.loh.combat;

import org.springframework.data.jpa.repository.JpaSpecificationExecutor;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface CombatRepository extends PagingAndSortingRepository<Combat, UUID>, JpaSpecificationExecutor<Combat> {
}
