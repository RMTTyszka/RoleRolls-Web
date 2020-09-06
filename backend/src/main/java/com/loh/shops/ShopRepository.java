package com.loh.shops;

import org.springframework.data.domain.Pageable;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.UUID;


// This will be AUTO IMPLEMENTED by Spring into a Bean called userRepository
// CRUD refers Create, Read, Update, Delete
public interface ShopRepository extends PagingAndSortingRepository<Shop, UUID> {

	Shop findBySystemDefaultAndName(boolean isDefault, String name);
	List<Shop> findAllByNameIgnoreCaseContaining (String name);
	List<Shop> findAllByNameIgnoreCaseContaining(String name, Pageable paged);

}