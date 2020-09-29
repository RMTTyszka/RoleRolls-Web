package com.loh.items.equipable.head.headpieceModel;

import com.loh.shared.LegacyBaseRepository;

public interface HeadpieceModelRepository extends LegacyBaseRepository<HeadpieceModel> {
    HeadpieceModel getByNameAndSystemDefaultTrue(String name);
}
