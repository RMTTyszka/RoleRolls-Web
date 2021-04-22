package com.rolerolls.domain.items.equipables.necks.base;

import com.rolerolls.shared.LegacyBaseRepository;

public interface BaseNeckAcessoryRepository extends LegacyBaseRepository<BaseNeckAccessory> {
    BaseNeckAccessory getByNameAndSystemDefaultTrue(String name);
}
