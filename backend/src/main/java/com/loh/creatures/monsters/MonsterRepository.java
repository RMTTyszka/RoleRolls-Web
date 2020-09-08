package com.loh.creatures.monsters;

import com.loh.shared.BaseRepository;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

public interface MonsterRepository extends BaseRepository<Monster>, JpaSpecificationExecutor<Monster> {
    Monster findByName(String name);
    Long deleteAllByNameContaining(String name);
}
