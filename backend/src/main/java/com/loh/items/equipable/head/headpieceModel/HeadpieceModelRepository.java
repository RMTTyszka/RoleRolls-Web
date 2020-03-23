package com.loh.items.equipable.head.headpieceModel;

import com.loh.shared.BaseRepository;

public interface HeadpieceModelRepository extends BaseRepository<HeadpieceModel> {
    HeadpieceModel getByNameAndSystemDefaultTrue(String name);
}
