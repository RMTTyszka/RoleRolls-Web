package com.loh.creatures.heroes.dtos;

import com.loh.race.Race;
import com.loh.role.Role;

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
