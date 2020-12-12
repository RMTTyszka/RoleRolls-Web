package com.loh.domain.items.equipables.gloves.base;

import com.loh.shared.LegacyBaseRepository;

public interface BaseGloveRepository extends LegacyBaseRepository<BaseGlove> {
    BaseGlove getByNameAndSystemDefaultTrue(String name);
}
