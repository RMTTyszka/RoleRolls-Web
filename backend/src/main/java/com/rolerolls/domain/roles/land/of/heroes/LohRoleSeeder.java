package com.rolerolls.domain.roles.land.of.heroes;

import com.rolerolls.domain.creatures.CreatureType;
import com.rolerolls.domain.roles.DefaultRole;
import com.rolerolls.domain.roles.Role;
import com.rolerolls.domain.roles.RoleRepository;
import com.rolerolls.domain.universes.UniverseType;
import com.rolerolls.shared.Bonus;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
public class LohRoleSeeder {
    @Autowired
    RoleRepository roleRepository;
    public void seed() {
        for (DefaultRole defaultRole : LohDefaultHeroRoles.roles) {
            Role role = roleRepository.findByNameAndUniverseTypeAndSystemDefaultTrue(defaultRole.name, UniverseType.LandOfHeroes);
            if (role == null) {
                List<Bonus> bonuses = new ArrayList<>();
                for (Bonus bonus : defaultRole.bonuses) {
                    bonuses.add(bonus);
                }
                role = new Role(defaultRole.name, bonuses, CreatureType.Hero, UniverseType.LandOfHeroes, true);
                roleRepository.save(role);
            } else {
                List<Bonus> bonuses = new ArrayList<>();
                for (Bonus bonus : defaultRole.bonuses) {
                    bonuses.add(bonus);
                }
                role.setBonuses(bonuses);
                roleRepository.save(role);
            }
        }
    }
}
