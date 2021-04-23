package com.rolerolls.domain.items.equipables.belts.models;

import com.rolerolls.shared.LegacyBaseRepository;

public interface BeltModelsRepository extends LegacyBaseRepository<BeltModel> {
    BeltModel getByNameAndSystemDefaultTrue(String name);
}
