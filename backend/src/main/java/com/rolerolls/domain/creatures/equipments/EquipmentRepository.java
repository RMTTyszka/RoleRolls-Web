package com.rolerolls.domain.creatures.equipments;

import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface EquipmentRepository extends PagingAndSortingRepository<Equipment, UUID> {
}
