package com.loh.items.equipable.armors.armorInstance;

import com.loh.items.equipable.armors.armorCategories.ArmorCategory;
import org.springframework.data.domain.Pageable;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.UUID;


public interface ArmorInstanceRepository extends PagingAndSortingRepository<ArmorInstance, UUID> {
    ArmorInstance findByArmorModel_BaseArmor_Category(ArmorCategory category);
    List<ArmorInstance> findAllByNameIgnoreCaseContaining(String name);
    List<ArmorInstance> findAllByNameIgnoreCaseContaining(String name, Pageable paged);
    ArmorInstance findByName(String name);
}
