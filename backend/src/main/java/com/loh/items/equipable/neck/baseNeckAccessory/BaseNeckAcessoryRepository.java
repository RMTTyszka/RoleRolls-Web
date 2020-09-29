package com.loh.items.equipable.neck.baseNeckAccessory;

import com.loh.shared.LegacyBaseRepository;

public interface BaseNeckAcessoryRepository extends LegacyBaseRepository<BaseNeckAccessory> {
    BaseNeckAccessory getByNameAndSystemDefaultTrue(String name);
}
