package com.loh.creatures;

import lombok.Getter;
import lombok.Setter;

public class Attacker {
    @Getter @Setter
    private Creature attacker;
    public Attacker(Creature attacker) {
        this.attacker = attacker;
    }
}
