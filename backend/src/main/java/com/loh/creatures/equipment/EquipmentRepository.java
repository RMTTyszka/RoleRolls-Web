package com.loh.creatures.equipment;

import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface EquipmentRepository extends PagingAndSortingRepository<Equipment, UUID> {
}
