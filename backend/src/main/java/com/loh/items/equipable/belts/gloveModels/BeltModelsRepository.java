package com.loh.items.equipable.belts.gloveModels;

import com.loh.shared.BaseRepository;

public interface BeltModelsRepository extends BaseRepository<BeltModel> {
    BeltModel getByNameAndSystemDefaultTrue(String name);
}
