package com.loh.items.equipable.rings.head.ringModels;

import com.loh.shared.LegacyBaseRepository;

public interface RingModelRepository extends LegacyBaseRepository<RingModel> {
    RingModel getByNameAndSystemDefaultTrue(String name);
}
