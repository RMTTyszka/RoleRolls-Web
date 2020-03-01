package com.loh.creatures.heroes;

import com.loh.creatures.Attributes;
import com.loh.creatures.Creature;
import com.loh.creatures.heroes.equipment.Equipment;
import com.loh.creatures.heroes.inventory.Inventory;
import com.loh.race.Race;
import com.loh.role.Role;

import javax.persistence.Entity;
import java.util.UUID;

@Entity
public class Hero extends Creature {


    public Hero(){
        super();
        id = UUID.randomUUID();
        level = 1;
        baseAttributes = new Attributes(8);
        bonusAttributes = new Attributes();
        name = "new hero";
        race = new Race();
        role = new Role();
        equipment = new Equipment();
        inventory = new Inventory();
    }
}
