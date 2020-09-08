package com.loh.authentication;

import org.springframework.data.jpa.repository.JpaSpecificationExecutor;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface UserRepository extends PagingAndSortingRepository<User, UUID>, JpaSpecificationExecutor<User> {
    User findByEmail(String email);
    boolean existsByEmailOrUserName(String email, String userName);
}
