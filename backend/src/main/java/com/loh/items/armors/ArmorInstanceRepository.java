package com.loh.items.armors;

import com.loh.items.armors.armorTypes.ArmorType;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;


public interface ArmorInstanceRepository extends PagingAndSortingRepository<ArmorInstance, UUID> {
    ArmorInstance findByArmorModel_BaseArmor_Category_ArmorType(ArmorType category);
}
