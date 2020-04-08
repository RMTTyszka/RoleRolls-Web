package com.loh.creatures.monsters;

import com.loh.creatures.Creature;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;
import javax.persistence.OneToOne;

@Entity
@DiscriminatorValue("Monster")
public class Monster extends Creature {

    @OneToOne
    @Getter
    @Setter
    private MonsterBase monsterBase;
}
