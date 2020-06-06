package com.loh.creatures;

import org.springframework.data.jpa.repository.JpaSpecificationExecutor;
import org.springframework.data.repository.CrudRepository;

import java.util.UUID;

public interface CreatureRepository extends CrudRepository<Creature, UUID>, JpaSpecificationExecutor<Creature> {
    Creature findByName(String name);
    Integer deleteByNameContaining(String name);
}

