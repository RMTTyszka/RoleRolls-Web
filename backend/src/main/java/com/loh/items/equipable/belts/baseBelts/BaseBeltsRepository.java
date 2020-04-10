package com.loh.items.equipable.belts.baseBelts;

import com.loh.shared.BaseRepository;

public interface BaseBeltsRepository extends BaseRepository<BaseBelt> {
    BaseBelt getByNameAndSystemDefaultTrue(String name);
}
