package com.rolerolls.domain.creatures.monsters;

import com.rolerolls.domain.creatures.Attributes;
import com.rolerolls.domain.creatures.Creature;
import com.rolerolls.domain.creatures.CreatureType;
import com.rolerolls.domain.creatures.equipments.Equipment;
import com.rolerolls.domain.creatures.inventory.Inventory;
import com.rolerolls.domain.creatures.monsters.models.MonsterModel;
import com.rolerolls.domain.creatures.monsters.models.MonsterTemplateSkill;
import com.rolerolls.domain.races.Race;
import com.rolerolls.domain.roles.Role;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import javax.persistence.ManyToOne;
import java.util.UUID;

@Entity
@DiscriminatorValue("Monster")
public class Monster extends Creature {

    @Getter
    @Setter
    @ManyToOne
    private MonsterModel monsterModel;
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
    @Override
    public void levelUp() {
        super.levelUp();
        for (MonsterTemplateSkill skill : monsterModel.getSkills()) {
            skills.addMajorSkillPoint(skill.getMajorSkill());

            skills.addMinorSkillPoint(skill.getMinorSkill1());
            skills.addMinorSkillPoint(skill.getMinorSkill2());
            skills.addMinorSkillPoint(skill.getMinorSkill3());
        }
        for (String attribute : monsterModel.getMainAttributes()) {
            bonusAttributes.levelUp(attribute);
        }
    }
}
