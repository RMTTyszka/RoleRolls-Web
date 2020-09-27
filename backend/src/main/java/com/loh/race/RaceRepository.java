package com.loh.race;

import com.loh.shared.BaseRepository;
import org.springframework.data.domain.Pageable;

import java.util.List;


// This will be AUTO IMPLEMENTED by Spring into a Bean called userRepository
// CRUD refers Create, Read, Update, Delete
public interface RaceRepository extends BaseRepository<Race> {

	List<Race> findAllByNameIgnoreCaseContaining(String name);
	List<Race> findAllByNameIgnoreCaseContaining(String name, Pageable paged);

	List<Race> findByPowersId(int powerId);

	Race findByNameAndSystemDefaultTrue(String name);
}