package com.loh.application.creatures.heroes.dtos;

import com.loh.domain.races.Race;
import com.loh.domain.roles.Role;

import java.util.UUID;

public class NewHeroDto {
    public String name;
    public Race race;
    public Role role;
    public UUID ownerId;

    public NewHeroDto() {
        race = new Race();
        role = new Role();
        name = "New Hero";
    }
}
