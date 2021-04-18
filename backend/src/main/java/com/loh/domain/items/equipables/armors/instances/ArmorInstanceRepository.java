package com.loh.domain.items.equipables.armors.instances;

import com.loh.domain.items.equipables.armors.categories.ArmorCategory;
import org.springframework.data.domain.Pageable;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.UUID;


public interface ArmorInstanceRepository extends PagingAndSortingRepository<ArmorInstance, UUID> {
    ArmorInstance findByArmorTemplate_BaseArmor_Category(ArmorCategory category);
    List<ArmorInstance> findAllByNameIgnoreCaseContaining(String name);
    List<ArmorInstance> findAllByNameIgnoreCaseContaining(String name, Pageable paged);
    ArmorInstance findByName(String name);
}
