package com.loh.items.equipable.neck.baseNeckAccessory;

import com.loh.shared.BaseRepository;

public interface BaseNeckAcessoryRepository extends BaseRepository<BaseNeckAccessory> {
    BaseNeckAccessory getByNameAndSystemDefaultTrue(String name);
}
