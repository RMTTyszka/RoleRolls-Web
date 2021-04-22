package com.rolerolls.domain.items.equipables.armors.templates;

import org.springframework.data.domain.Pageable;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.UUID;


public interface ArmorTemplateRepository extends PagingAndSortingRepository<ArmorTemplate, UUID> {
    List<ArmorTemplate> findAllByNameIgnoreCaseContaining(String name);
    List<ArmorTemplate> findAllByNameIgnoreCaseContaining(String name, Pageable paged);
    ArmorTemplate findByNameAndSystemDefaultTrue(String name);
    ArmorTemplate findByName(String name);
    boolean existsByNameAndSystemDefaultIsTrue(String name);
}
