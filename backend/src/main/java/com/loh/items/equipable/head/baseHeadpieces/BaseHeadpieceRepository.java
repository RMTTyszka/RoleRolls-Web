package com.loh.items.equipable.head.baseHeadpieces;

import com.loh.shared.LegacyBaseRepository;

public interface BaseHeadpieceRepository extends LegacyBaseRepository<BaseHeadpiece> {
    BaseHeadpiece getByNameAndSystemDefaultTrue(String name);
}
