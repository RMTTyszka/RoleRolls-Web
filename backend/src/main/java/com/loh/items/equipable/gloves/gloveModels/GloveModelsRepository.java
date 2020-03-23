package com.loh.items.equipable.gloves.gloveModels;

import org.springframework.data.domain.Pageable;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.UUID;

public interface GloveModelsRepository extends PagingAndSortingRepository<GloveModel, UUID> {
    GloveModel getByNameAndSystemDefaultTrue(String name);
    List<GloveModel> findAllByNameIgnoreCaseContaining(String name);
    List<GloveModel> findAllByNameIgnoreCaseContaining(String name, Pageable pageable);

}
