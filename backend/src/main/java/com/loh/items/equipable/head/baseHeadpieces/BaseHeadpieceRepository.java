package com.loh.items.equipable.head.baseHeadpieces;

import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface BaseHeadpieceRepository extends PagingAndSortingRepository<BaseHeadpiece, UUID> {
    BaseHeadpiece getByNameAndSystemDefaultTrue(String name);
}
