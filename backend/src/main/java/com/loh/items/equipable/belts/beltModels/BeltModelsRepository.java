package com.loh.items.equipable.belts.beltModels;

import com.loh.shared.LegacyBaseRepository;

public interface BeltModelsRepository extends LegacyBaseRepository<BeltModel> {
    BeltModel getByNameAndSystemDefaultTrue(String name);
}
