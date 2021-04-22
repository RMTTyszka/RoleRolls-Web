package com.rolerolls.domain.roles.dummy;

import com.rolerolls.domain.creatures.CreatureType;
import com.rolerolls.domain.roles.Role;
import com.rolerolls.domain.roles.RoleRepository;
import com.rolerolls.domain.universes.UniverseType;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class DummyRoleSeeder {
    @Autowired
    RoleRepository roleRepository;
    public void seed() {
        Role role = roleRepository.findByNameAndUniverseTypeAndSystemDefaultTrue("Dummy", UniverseType.LandOfHeroes);
        if (role == null) {
            role = new Role("Dummy", null, CreatureType.Hero, UniverseType.Dummy, true);
            roleRepository.save(role);
        }
    }
}
