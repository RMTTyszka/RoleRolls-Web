package com.loh.creatures.heroes;

import org.springframework.data.jpa.repository.JpaSpecificationExecutor;
import org.springframework.data.repository.CrudRepository;

import java.util.UUID;

public interface HeroRepository extends CrudRepository<Hero, UUID>, JpaSpecificationExecutor<Hero> {
    Hero findByName(String name);
    Long deleteAllByNameContaining(String name);
}
