package com.loh.domain.items.equipables;

import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface EquipableTemplateRepository extends PagingAndSortingRepository<EquipableTemplate, UUID> {

}
