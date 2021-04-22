package com.rolerolls.domain.races;

import com.rolerolls.domain.universes.UniverseType;
import com.rolerolls.shared.BaseRepository;
import org.springframework.data.domain.Pageable;

import java.util.List;


// This will be AUTO IMPLEMENTED by Spring into a Bean called userRepository
// CRUD refers Create, Read, Update, Delete
public interface RaceRepository extends BaseRepository<Race> {

	List<Race> findAllByNameIgnoreCaseContaining(String name);
	List<Race> findAllByNameIgnoreCaseContaining(String name, Pageable paged);

	List<Race> findByPowersId(int powerId);

	Race findByNameAndUniverseTypeAndSystemDefaultTrue(String name, UniverseType universeType);
}