package com.loh.creatures.inventory;

import com.loh.creatures.Attributes;
import com.loh.creatures.equipment.Equipment;
import com.loh.race.Race;
import com.loh.role.Role;

import java.util.UUID;

public class NewHeroDto {
    public UUID id = UUID.randomUUID();
    public int level = 1;
    public Attributes baseAttributes = new Attributes(8);
    public Attributes bonusAttributes = new Attributes();
    public String name = "new hero";
    public Equipment equipment = new Equipment();
    public Inventory inventory = new Inventory();
    public Race race = new Race();
    public Role role = new Role();


}
