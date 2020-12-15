package com.loh.domain.items.equipables.belts.models;

import com.loh.shared.LegacyBaseRepository;

public interface BeltModelsRepository extends LegacyBaseRepository<BeltModel> {
    BeltModel getByNameAndSystemDefaultTrue(String name);
}
