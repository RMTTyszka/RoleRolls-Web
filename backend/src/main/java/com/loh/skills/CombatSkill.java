package com.loh.skills;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;

@Entity
@DiscriminatorValue("Combat")
public class CombatSkill extends Skill {
    public CombatSkill() {
        super();
    }

    @Override
    protected Integer getUsedPoints() {
        return attack + specialAttack + power + evasion;
    }

    @Getter @Setter
    private Integer attack = 0;
    @Getter @Setter
    private Integer specialAttack = 0;
    @Getter @Setter
    private Integer power = 0;
    @Getter @Setter
    private Integer evasion = 0;
}
