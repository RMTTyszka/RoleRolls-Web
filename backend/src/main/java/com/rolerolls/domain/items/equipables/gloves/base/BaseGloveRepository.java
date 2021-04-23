package com.rolerolls.domain.items.equipables.gloves.base;

import com.rolerolls.shared.LegacyBaseRepository;

public interface BaseGloveRepository extends LegacyBaseRepository<BaseGlove> {
    BaseGlove getByNameAndSystemDefaultTrue(String name);
}
