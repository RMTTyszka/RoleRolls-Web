package com.loh.race;

import org.springframework.data.domain.Pageable;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.UUID;


// This will be AUTO IMPLEMENTED by Spring into a Bean called userRepository
// CRUD refers Create, Read, Update, Delete
public interface RaceRepository extends PagingAndSortingRepository<Race, UUID> {

	List<Race> findAllByNameIgnoreCaseContaining(String name);
	List<Race> findAllByNameIgnoreCaseContaining(String name, Pageable paged);

	List<Race> findByPowersId(int powerId);

	Race findByNameAndSystemDefaultTrue(String name);
}