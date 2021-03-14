package com.loh.domain.roles.the.future.is.out.there.heroes;

import com.loh.domain.creatures.CreatureType;
import com.loh.domain.roles.DefaultRole;
import com.loh.domain.roles.Role;
import com.loh.domain.roles.RoleRepository;
import com.loh.domain.universes.UniverseType;
import com.loh.shared.Bonus;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
public class FotRoleSeeder {
    @Autowired
    RoleRepository roleRepository;
    public void seed() {
        for (DefaultRole defaultRole : FotDefaultHeroRoles.roles) {
            Role role = roleRepository.findByNameAndUniverseTypeAndSystemDefaultTrue(defaultRole.name, UniverseType.TheFutureIsOutThere);
            if (role == null) {
                List<Bonus> bonuses = new ArrayList<>();
                for (Bonus bonus : defaultRole.bonuses) {
                    bonuses.add(bonus);
                }
                role = new Role(defaultRole.name, bonuses, CreatureType.Hero, UniverseType.TheFutureIsOutThere, true);
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
        for (DefaultRole defaultRole : FotDefaultMonsterRoles.roles) {
            Role role = roleRepository.findByNameAndUniverseTypeAndSystemDefaultTrue(defaultRole.name, UniverseType.TheFutureIsOutThere);
            if (role == null) {
                List<Bonus> bonuses = new ArrayList<>();
                for (Bonus bonus : defaultRole.bonuses) {
                    bonuses.add(bonus);
                }
                role = new Role(defaultRole.name, bonuses, CreatureType.Monster, UniverseType.TheFutureIsOutThere, true);
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
