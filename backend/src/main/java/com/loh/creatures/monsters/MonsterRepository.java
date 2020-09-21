package com.loh.creatures.monsters;

import com.loh.shared.BaseRepository;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaSpecificationExecutor;

import java.util.UUID;

public interface MonsterRepository extends BaseRepository<Monster>, JpaSpecificationExecutor<Monster> {
    Monster findByName(String name);
    Page<Monster> findAllByNameIgnoreCaseContainingAndOwnerId(String name, UUID ownerId, Pageable paged);
    Page<Monster> findAllByOwnerId(UUID ownerId, Pageable paged);
    Long deleteAllByNameContaining(String name);
}
