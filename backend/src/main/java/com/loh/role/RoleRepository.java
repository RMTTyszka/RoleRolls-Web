package com.loh.role;

import org.springframework.data.domain.Pageable;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.UUID;

public interface RoleRepository extends PagingAndSortingRepository<Role, UUID> {

    List<Role> findAllByNameIgnoreCaseContaining(String name);
    List<Role> findAllByNameIgnoreCaseContaining(String name, Pageable paged);
    Role findByNameAndSystemDefaultTrue(String name);

}
