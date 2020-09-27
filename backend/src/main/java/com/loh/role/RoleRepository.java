package com.loh.role;

import com.loh.shared.BaseRepository;
import org.springframework.data.domain.Pageable;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.UUID;

public interface RoleRepository extends BaseRepository<Role> {

    List<Role> findAllByNameIgnoreCaseContaining(String name);
    List<Role> findAllByNameIgnoreCaseContaining(String name, Pageable paged);
    Role findByNameAndSystemDefaultTrue(String name);

}
