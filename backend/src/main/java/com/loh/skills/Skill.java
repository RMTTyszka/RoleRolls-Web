package com.loh.skills;

import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.util.Arrays;
import java.util.List;

@javax.persistence.Entity
@Inheritance(strategy = InheritanceType.SINGLE_TABLE)
@DiscriminatorColumn(name = "MainSkill", discriminatorType = DiscriminatorType.STRING)
public abstract class Skill extends Entity {

    public Skill() {
        level = 1;
    }

    @Getter @Setter
    private Integer level;
    @Getter @Setter
    private Integer points = 0;


    public Integer getMaxPointPerSkill() {
        return level / 2 + 2;
    }
    public Integer getTotalPoints() {
        return level * 3;
    }
    public Integer getRemainingPoints() {
        return getTotalPoints() - getUsedPoints();
    }

    public void levelUp() {
        this.level++;
    }

    protected abstract Integer getUsedPoints();

    public static List<String> getList(){
        return Arrays.asList("jump", "climb", "athleticism", "steal", "stealth",
                "operateMechanisms", "diplomacy", "bluff", "intimidate", "arcane","religion",
                "nature","perceive","feeling","search","perceive","feeling","search",
                "mysticism","toughness","reflex", "attack","specialAttack","power", "evasion");
    }
}
