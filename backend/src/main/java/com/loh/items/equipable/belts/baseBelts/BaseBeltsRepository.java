package com.loh.items.equipable.belts.baseBelts;

import com.loh.shared.LegacyBaseRepository;

public interface BaseBeltsRepository extends LegacyBaseRepository<BaseBelt> {
    BaseBelt getByNameAndSystemDefaultTrue(String name);
}
