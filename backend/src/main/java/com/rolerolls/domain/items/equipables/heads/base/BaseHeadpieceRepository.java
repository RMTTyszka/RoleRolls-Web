package com.rolerolls.domain.items.equipables.heads.base;

import com.rolerolls.shared.LegacyBaseRepository;

public interface BaseHeadpieceRepository extends LegacyBaseRepository<BaseHeadpiece> {
    BaseHeadpiece getByNameAndSystemDefaultTrue(String name);
}
