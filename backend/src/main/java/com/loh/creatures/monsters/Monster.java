package com.loh.creatures.monsters;

import com.loh.creatures.Attributes;
import com.loh.creatures.Creature;
import com.loh.creatures.CreatureType;
import com.loh.creatures.equipment.Equipment;
import com.loh.creatures.inventory.Inventory;
import com.loh.race.Race;
import com.loh.role.Role;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import javax.persistence.OneToOne;
import java.util.UUID;

@Entity
@DiscriminatorValue("Monster")
public class Monster extends Creature {

    @OneToOne
    @Getter
    @Setter
    private MonsterBase monsterBase;
    @Override
    protected CreatureType getCreatureType() {
        return CreatureType.Monster;
    }
    public Monster(){
        super();
        id = UUID.randomUUID();
        level = 1;
        baseAttributes = new Attributes(8);
        bonusAttributes = new Attributes();
        name = "new hero";
        equipment = new Equipment();
        inventory = new Inventory();
    }
    public Monster(String name, Race race, Role role){
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
    }
    public Monster(String name){
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
