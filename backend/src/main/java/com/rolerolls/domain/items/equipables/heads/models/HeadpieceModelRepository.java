package com.rolerolls.domain.items.equipables.heads.models;

import com.rolerolls.shared.LegacyBaseRepository;

public interface HeadpieceModelRepository extends LegacyBaseRepository<HeadpieceModel> {
    HeadpieceModel getByNameAndSystemDefaultTrue(String name);
}
