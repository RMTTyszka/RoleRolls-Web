package com.loh.items.equipable.belts.beltModels;

import com.loh.shared.BaseRepository;

public interface BeltModelsRepository extends BaseRepository<BeltModel> {
    BeltModel getByNameAndSystemDefaultTrue(String name);
}
