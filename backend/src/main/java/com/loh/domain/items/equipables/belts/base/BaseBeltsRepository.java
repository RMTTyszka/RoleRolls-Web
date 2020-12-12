package com.loh.domain.items.equipables.belts.base;

import com.loh.shared.LegacyBaseRepository;

public interface BaseBeltsRepository extends LegacyBaseRepository<BaseBelt> {
    BaseBelt getByNameAndSystemDefaultTrue(String name);
}
