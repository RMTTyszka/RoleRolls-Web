package com.loh.creatures.heroes.equipment;

import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface EquipmentRepository extends PagingAndSortingRepository<Equipment, UUID> {
}
