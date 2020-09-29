package com.loh.items.equipable.neck.neckAccessoryModels;

import com.loh.shared.LegacyBaseRepository;

public interface NeckAccessoryModelRepository extends LegacyBaseRepository<NeckAcessoryModel> {
    NeckAcessoryModel getByNameAndSystemDefaultTrue(String name);
}
