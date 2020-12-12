package com.loh.domain.items.equipables.armors.models;

import org.springframework.data.domain.Pageable;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.UUID;


public interface ArmorModelRepository extends PagingAndSortingRepository<ArmorModel, UUID> {
    List<ArmorModel> findAllByNameIgnoreCaseContaining(String name);
    List<ArmorModel> findAllByNameIgnoreCaseContaining(String name, Pageable paged);
    ArmorModel findByNameAndSystemDefaultTrue(String name);
    ArmorModel findByName(String name);
}
