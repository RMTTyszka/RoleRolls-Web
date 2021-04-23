package com.rolerolls.domain.creatures.heroes;

import com.rolerolls.domain.creatures.Attributes;
import com.rolerolls.domain.creatures.Creature;
import com.rolerolls.domain.creatures.CreatureType;
import com.rolerolls.domain.creatures.equipments.Equipment;
import com.rolerolls.domain.creatures.inventory.Inventory;
import com.rolerolls.domain.races.Race;
import com.rolerolls.domain.roles.Role;
import com.rolerolls.domain.skills.CreatureSkills;

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

    public static Hero NewHero() {
        Hero hero = new Hero();
        hero.id = UUID.randomUUID();
        hero.level = 1;
        hero.baseAttributes = new Attributes(8);
        hero.bonusAttributes = new Attributes();
        hero.name = "new hero";
        hero.equipment = new Equipment();
        hero.inventory = new Inventory();
        hero.inventory.setCash1(100);
        return hero;
    }

    public Hero(){
        super();
    }
    public Hero(String name, Race race, Role role, UUID ownerId, UUID creatorId){
        super();
        id = UUID.randomUUID();
        level = 1;
        baseAttributes = new Attributes(8);
        bonusAttributes = new Attributes();
        skills = new CreatureSkills();
        this.name = name;
        this.race = race;
        this.role = role;
        equipment = new Equipment();
        inventory = new Inventory();
        this.inventory.setCash1(100);
        this.ownerId = ownerId;
        this.creatorId = creatorId;
        setCurrentLife(this.getStatus().getLife());
        setCurrentMoral(this.getStatus().getMoral());
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
