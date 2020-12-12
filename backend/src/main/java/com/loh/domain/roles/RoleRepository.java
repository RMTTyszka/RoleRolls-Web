package com.loh.domain.roles;

import com.loh.shared.BaseRepository;
import org.springframework.data.domain.Pageable;

import java.util.List;

public interface RoleRepository extends BaseRepository<Role> {

    List<Role> findAllByNameIgnoreCaseContaining(String name);
    List<Role> findAllByNameIgnoreCaseContaining(String name, Pageable paged);
    Role findByNameAndSystemDefaultTrue(String name);

}
