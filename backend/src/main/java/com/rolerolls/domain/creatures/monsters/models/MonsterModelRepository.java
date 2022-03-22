package com.rolerolls.domain.creatures.monsters.models;

import com.rolerolls.domain.campaigns.Campaign;
import com.rolerolls.shared.BaseRepository;

import java.util.List;
import java.util.UUID;

public interface MonsterModelRepository extends BaseRepository<MonsterModel> {
    List<MonsterModel> findAllByIdIn(List<UUID> ids);
}
