package com.loh.authentication;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;


@Service
public class AdminSeeder {

    @Autowired
    UserRepository userRepository;

    public void seed() {
        if (userRepository.findByEmail("ramiro.tyszka") == null) {
        }
    }
}
