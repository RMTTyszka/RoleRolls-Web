package com.loh.items.equipable.gloves.gloveModels;

import com.loh.shared.BaseRepository;

public interface GloveModelsRepository extends BaseRepository<GloveModel> {
    GloveModel getByNameAndSystemDefaultTrue(String name);
}
