package com.loh.creatures.heroes;

import com.loh.creatures.Attributes;
import com.loh.creatures.Creature;
import com.loh.creatures.CreatureType;
import com.loh.creatures.equipment.Equipment;
import com.loh.creatures.inventory.Inventory;
import com.loh.race.Race;
import com.loh.role.Role;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Column;
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
    @Getter @Setter
    @Column(columnDefinition = "BINARY(16)")
    private UUID ownerId;
    @Getter @Setter
    @Column(columnDefinition = "BINARY(16)")
    private UUID creatorId;

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
