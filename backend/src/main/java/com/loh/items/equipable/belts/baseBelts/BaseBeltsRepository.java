package com.loh.items.equipable.belts.baseBelts;

import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface BaseBeltsRepository extends PagingAndSortingRepository<BaseBelt, UUID> {
    BaseBelt getByNameAndSystemDefaultTrue(String name);
}
