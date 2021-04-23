package com.rolerolls.domain.shops;

import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;


// This will be AUTO IMPLEMENTED by Spring into a Bean called userRepository
// CRUD refers Create, Read, Update, Delete
public interface ShopArmorRepository extends PagingAndSortingRepository<ShopArmor, UUID> {

}