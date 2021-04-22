package com.rolerolls.domain.items.equipables.belts.base;

import com.rolerolls.shared.LegacyBaseRepository;

public interface BaseBeltsRepository extends LegacyBaseRepository<BaseBelt> {
    BaseBelt getByNameAndSystemDefaultTrue(String name);
}
