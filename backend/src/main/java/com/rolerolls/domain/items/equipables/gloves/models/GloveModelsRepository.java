package com.rolerolls.domain.items.equipables.gloves.models;

import com.rolerolls.shared.LegacyBaseRepository;

public interface GloveModelsRepository extends LegacyBaseRepository<GloveModel> {
    GloveModel getByNameAndSystemDefaultTrue(String name);
}
