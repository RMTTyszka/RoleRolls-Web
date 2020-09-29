package com.loh.creatures.heroes;

import com.loh.shared.BaseRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

public interface HeroRepository extends BaseRepository<Hero>, JpaSpecificationExecutor<Hero> {
    Hero findByName(String name);
    Long deleteAllByNameContaining(String name);
}
