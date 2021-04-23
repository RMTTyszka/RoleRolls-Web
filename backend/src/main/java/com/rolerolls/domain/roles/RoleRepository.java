package com.rolerolls.domain.roles;

import com.rolerolls.domain.universes.UniverseType;
import com.rolerolls.shared.BaseRepository;
import org.springframework.data.domain.Pageable;

import java.util.List;

public interface RoleRepository extends BaseRepository<Role> {

    List<Role> findAllByNameIgnoreCaseContaining(String name);
    List<Role> findAllByNameIgnoreCaseContaining(String name, Pageable paged);
    Role findByNameAndUniverseTypeAndSystemDefaultTrue(String name, UniverseType universeType);

}
