package com.rolerolls.domain.creatures.monsters.models;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.Embeddable;

@Embeddable
public class MonsterTemplateSkill {
    @Getter @Setter
    private String majorSkill;
    @Getter @Setter
    private String minorSkill1;
    @Getter @Setter
    private String minorSkill2;
    @Getter @Setter
    private String minorSkill3;

}
