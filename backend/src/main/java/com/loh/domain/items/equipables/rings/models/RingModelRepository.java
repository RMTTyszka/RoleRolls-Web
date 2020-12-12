package com.loh.domain.items.equipables.rings.models;

import com.loh.shared.LegacyBaseRepository;

public interface RingModelRepository extends LegacyBaseRepository<RingModel> {
    RingModel getByNameAndSystemDefaultTrue(String name);
}
