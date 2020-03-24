package com.loh.items.equipable.neck.baseNeckAccessory;

import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface BaseNeckAcessoryRepository extends PagingAndSortingRepository<BaseNeckAccessory, UUID> {
    BaseNeckAccessory getByNameAndSystemDefaultTrue(String name);
}
