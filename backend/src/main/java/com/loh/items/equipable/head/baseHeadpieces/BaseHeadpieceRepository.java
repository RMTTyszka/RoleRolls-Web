package com.loh.items.equipable.head.baseHeadpieces;

import com.loh.shared.BaseRepository;

public interface BaseHeadpieceRepository extends BaseRepository<BaseHeadpiece> {
    BaseHeadpiece getByNameAndSystemDefaultTrue(String name);
}
