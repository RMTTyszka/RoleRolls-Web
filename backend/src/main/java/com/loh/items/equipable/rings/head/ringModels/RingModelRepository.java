package com.loh.items.equipable.rings.head.ringModels;

import com.loh.shared.BaseRepository;

public interface RingModelRepository extends BaseRepository<RingModel> {
    RingModel getByNameAndSystemDefaultTrue(String name);
}
