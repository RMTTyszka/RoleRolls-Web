package com.rolerolls.domain.items.equipables.necks.models;

import com.rolerolls.shared.LegacyBaseRepository;

public interface NeckAccessoryModelRepository extends LegacyBaseRepository<NeckAcessoryModel> {
    NeckAcessoryModel getByNameAndSystemDefaultTrue(String name);
}
