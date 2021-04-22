package com.rolerolls.application.creatures.heroes.dtos;

import com.rolerolls.domain.races.Race;
import com.rolerolls.domain.roles.Role;

import java.util.UUID;

public class NewHeroDto {
    public String name;
    public Integer level;
    public Race race;
    public Role role;
    public UUID ownerId;

    public NewHeroDto() {
        race = new Race();
        role = new Role();
        name = "New Hero";
    }
}
