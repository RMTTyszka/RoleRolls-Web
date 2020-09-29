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

import javax.persistence.Column;
import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import java.util.UUID;

@Entity
@DiscriminatorValue("Monster")
public class Monster extends Creature {

    @Getter
    @Setter
    @Column(columnDefinition = "BINARY(16)")
    private UUID monsterModelId;
    @Override
    protected CreatureType getCreatureType() {
        return CreatureType.Monster;
    }
    public Monster(){
        super();
        id = UUID.randomUUID();
        level = 1;
        name = "new monster";
    }
    public Monster(String name, Race race, Role role, UUID creatorId, UUID ownerId){
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
    public Monster(String name, UUID creatorId, UUID ownerId){
        super();
        id = UUID.randomUUID();
        level = 1;
        baseAttributes = new Attributes(8);
        bonusAttributes = new Attributes();
        this.name = name;
        equipment = new Equipment();
        inventory = new Inventory();
        this.ownerId = ownerId;
        this.creatorId = creatorId;
    }
}
