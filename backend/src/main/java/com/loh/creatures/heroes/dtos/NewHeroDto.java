package com.loh.creatures.heroes.dtos;

import com.loh.race.Race;
import com.loh.role.Role;

public class NewHeroDto {
    public String name;
    public Race race;
    public Role role;

    public NewHeroDto() {
        race = new Race();
        role = new Role();
        name = "New Hero";
    }
}
