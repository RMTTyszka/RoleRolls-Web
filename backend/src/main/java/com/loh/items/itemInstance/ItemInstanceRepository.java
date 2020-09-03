package com.loh.items.itemInstance;

import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface ItemInstanceRepository extends PagingAndSortingRepository<ItemInstance, UUID> {
}