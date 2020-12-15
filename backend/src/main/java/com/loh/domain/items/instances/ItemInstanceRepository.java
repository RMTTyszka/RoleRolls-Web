package com.loh.domain.items.instances;

import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface ItemInstanceRepository extends PagingAndSortingRepository<ItemInstance, UUID> {
}