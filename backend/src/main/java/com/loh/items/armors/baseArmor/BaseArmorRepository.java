package com.loh.items.armors.baseArmor;

import com.loh.items.armors.armorTypes.ArmorType;
import org.springframework.data.domain.Pageable;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.UUID;

public interface BaseArmorRepository extends PagingAndSortingRepository<BaseArmor, UUID> {
    List<BaseArmor> findAllByNameIgnoreCaseContaining(String name);
    List<BaseArmor> findAllByNameIgnoreCaseContaining(String name, Pageable paged);
    BaseArmor findByCategory_ArmorType(ArmorType type);
}
