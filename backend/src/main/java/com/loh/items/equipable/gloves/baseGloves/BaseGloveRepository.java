package com.loh.items.equipable.gloves.baseGloves;

import com.loh.shared.LegacyBaseRepository;

public interface BaseGloveRepository extends LegacyBaseRepository<BaseGlove> {
    BaseGlove getByNameAndSystemDefaultTrue(String name);
}
