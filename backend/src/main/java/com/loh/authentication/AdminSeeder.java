package com.loh.authentication;

import com.loh.domain.contexts.Player;
import com.loh.domain.contexts.PlayerRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;


@Service
public class AdminSeeder {

    public static String adminEmail = "admin@rolerolls.com" ;

    @Autowired
    UserRepository userRepository;
    @Autowired
    PlayerRepository playerRepository;

    public void seed() {
        if (userRepository.findByEmail("admin@rolerolls.com") == null) {
            User admin = new User();
            admin.setEmail("admin@rolerolls.com");
            admin.setUserName("admin");
            admin.setFirstName("admin");
            admin.setLastName("RoleRolls");
            admin.setPassword("$2a$10$PXgTKD/zcmiIcUTAe8IvSOoZGXkfx/Ktn3Jv.hkhDCsweJL4yURJe");
            admin = userRepository.save(admin);
            Player player = new Player();
            player.setId(admin.getId());
            player.setName("admin");
            playerRepository.save(player);
        }
    }
}
