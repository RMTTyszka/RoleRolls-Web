package com.loh.domain.items.equipables.necks.base;

import com.loh.shared.LegacyBaseRepository;

public interface BaseNeckAcessoryRepository extends LegacyBaseRepository<BaseNeckAccessory> {
    BaseNeckAccessory getByNameAndSystemDefaultTrue(String name);
}
