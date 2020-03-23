package com.loh.items.equipable.gloves.baseGloves;

import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface BaseGloveRepository extends PagingAndSortingRepository<BaseGlove, UUID> {
    BaseGlove getByNameAndSystemDefaultTrue(String name);
}
