package com.rolerolls.domain.items.equipables.rings.models;

import com.rolerolls.shared.LegacyBaseRepository;

public interface RingModelRepository extends LegacyBaseRepository<RingModel> {
    RingModel getByNameAndSystemDefaultTrue(String name);
}
