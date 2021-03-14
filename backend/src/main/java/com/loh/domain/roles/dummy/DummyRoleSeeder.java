package com.loh.domain.roles.dummy;

import com.loh.domain.creatures.CreatureType;
import com.loh.domain.roles.Role;
import com.loh.domain.roles.RoleRepository;
import com.loh.domain.universes.UniverseType;
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
