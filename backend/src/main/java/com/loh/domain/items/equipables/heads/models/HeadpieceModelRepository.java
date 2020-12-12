package com.loh.domain.items.equipables.heads.models;

import com.loh.shared.LegacyBaseRepository;

public interface HeadpieceModelRepository extends LegacyBaseRepository<HeadpieceModel> {
    HeadpieceModel getByNameAndSystemDefaultTrue(String name);
}
