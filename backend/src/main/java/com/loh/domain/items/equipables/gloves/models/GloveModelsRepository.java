package com.loh.domain.items.equipables.gloves.models;

import com.loh.shared.LegacyBaseRepository;

public interface GloveModelsRepository extends LegacyBaseRepository<GloveModel> {
    GloveModel getByNameAndSystemDefaultTrue(String name);
}
