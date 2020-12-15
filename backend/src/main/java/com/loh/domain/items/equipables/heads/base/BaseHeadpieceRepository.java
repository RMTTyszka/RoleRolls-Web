package com.loh.domain.items.equipables.heads.base;

import com.loh.shared.LegacyBaseRepository;

public interface BaseHeadpieceRepository extends LegacyBaseRepository<BaseHeadpiece> {
    BaseHeadpiece getByNameAndSystemDefaultTrue(String name);
}
