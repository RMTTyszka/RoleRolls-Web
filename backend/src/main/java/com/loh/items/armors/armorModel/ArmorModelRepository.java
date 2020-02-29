package com.loh.items.armors.armorModel;

import com.loh.items.armors.armorCategories.ArmorCategoryEnum;
import org.springframework.data.domain.Pageable;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.UUID;


public interface ArmorModelRepository extends PagingAndSortingRepository<ArmorModel, UUID> {
    List<ArmorModel> findAllByNameIgnoreCaseContaining(String name);
    List<ArmorModel> findAllByNameIgnoreCaseContaining(String name, Pageable paged);
    ArmorModel findArmorByBaseArmor_Category_ArmorCategoryEnum(ArmorCategoryEnum category);
}
