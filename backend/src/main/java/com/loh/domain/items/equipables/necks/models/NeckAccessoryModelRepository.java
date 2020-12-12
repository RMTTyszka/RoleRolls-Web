package com.loh.domain.items.equipables.necks.models;

import com.loh.shared.LegacyBaseRepository;

public interface NeckAccessoryModelRepository extends LegacyBaseRepository<NeckAcessoryModel> {
    NeckAcessoryModel getByNameAndSystemDefaultTrue(String name);
}
