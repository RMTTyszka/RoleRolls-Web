package com.rolerolls.domain.items.equipables.armors.base;

import org.springframework.data.domain.Pageable;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.UUID;

public interface BaseArmorRepository extends PagingAndSortingRepository<BaseArmor, UUID> {
    List<BaseArmor> findAllByNameIgnoreCaseContaining(String name);
    List<BaseArmor> findAllByNameIgnoreCaseContaining(String name, Pageable paged);
    BaseArmor findByNameAndSystemDefaultTrue(String name);
    boolean existsByNameAndSystemDefaultIsTrue(String name);
}
