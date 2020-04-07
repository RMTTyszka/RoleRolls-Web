package com.loh.combat;

import org.springframework.data.jpa.repository.JpaSpecificationExecutor;
import org.springframework.data.repository.CrudRepository;

import java.util.UUID;

public interface CombatRepository extends CrudRepository<Combat, UUID>, JpaSpecificationExecutor<Combat> {
}
