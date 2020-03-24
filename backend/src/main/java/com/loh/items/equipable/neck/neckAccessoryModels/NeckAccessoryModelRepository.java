package com.loh.items.equipable.neck.neckAccessoryModels;

import com.loh.shared.BaseRepository;

public interface NeckAccessoryModelRepository extends BaseRepository<NeckAcessoryModel> {
    NeckAcessoryModel getByNameAndSystemDefaultTrue(String name);
}
