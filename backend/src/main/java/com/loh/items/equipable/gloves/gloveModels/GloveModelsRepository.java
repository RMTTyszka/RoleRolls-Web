package com.loh.items.equipable.gloves.gloveModels;

import com.loh.shared.LegacyBaseRepository;

public interface GloveModelsRepository extends LegacyBaseRepository<GloveModel> {
    GloveModel getByNameAndSystemDefaultTrue(String name);
}
