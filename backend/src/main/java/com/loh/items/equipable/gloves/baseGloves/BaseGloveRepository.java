package com.loh.items.equipable.gloves.baseGloves;

import com.loh.shared.BaseRepository;

public interface BaseGloveRepository extends BaseRepository<BaseGlove> {
    BaseGlove getByNameAndSystemDefaultTrue(String name);
}
