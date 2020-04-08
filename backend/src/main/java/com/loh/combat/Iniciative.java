package com.loh.combat;

import com.loh.creatures.Creature;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Embeddable;
import javax.persistence.OneToOne;

@Embeddable
public class Iniciative {
    @Getter
    @Setter
    @OneToOne
    private Creature creature;
    @Getter
    @Setter
    private Integer value;


}
