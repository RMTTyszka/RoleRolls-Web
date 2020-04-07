package com.loh.creatures.monsters;

import com.loh.creatures.Creature;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import javax.persistence.OneToMany;

@Entity
@DiscriminatorValue("Monster")
public class Monster extends Creature {

    @OneToMany
    private MonsterBase monsterBase;
}
