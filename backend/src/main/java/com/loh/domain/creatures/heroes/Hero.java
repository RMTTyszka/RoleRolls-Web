package com.loh.domain.creatures.heroes;

import com.loh.domain.creatures.Attributes;
import com.loh.domain.creatures.Creature;
import com.loh.domain.creatures.CreatureType;
import com.loh.domain.creatures.equipments.Equipment;
import com.loh.domain.creatures.inventory.Inventory;
import com.loh.domain.races.Race;
import com.loh.domain.roles.Role;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import java.util.UUID;

@Entity
@DiscriminatorValue("Hero")
public class Hero extends Creature {

    @Override
    protected CreatureType getCreatureType() {
        return CreatureType.Hero;
    }


    public Hero(){
        super();
        id = UUID.randomUUID();
        level = 1;
        baseAttributes = new Attributes(8);
        bonusAttributes = new Attributes();
        name = "new hero";
        equipment = new Equipment();
        inventory = new Inventory();
        inventory.setCash1(100);
    }
    public Hero(String name, Race race, Role role, UUID ownerId, UUID creatorId){
        super();
        id = UUID.randomUUID();
        level = 1;
        baseAttributes = new Attributes(8);
        bonusAttributes = new Attributes();
        this.name = name;
        this.race = race;
        this.role = role;
        equipment = new Equipment();
        inventory = new Inventory();
        this.ownerId = ownerId;
        this.creatorId = creatorId;
    }
    public Hero(String name){
        super();
        id = UUID.randomUUID();
        level = 1;
        baseAttributes = new Attributes(8);
        bonusAttributes = new Attributes();
        this.name = name;
        equipment = new Equipment();
        inventory = new Inventory();
    }
}
