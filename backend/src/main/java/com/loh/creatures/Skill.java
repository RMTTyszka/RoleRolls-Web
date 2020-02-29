package com.loh.creatures;

import com.loh.shared.Entity;

import java.util.Arrays;
import java.util.List;

@javax.persistence.Entity
public class Skill extends Entity {
    public Integer openLocks;
    public Integer steal;
    public Integer operateTrap;
    public Integer jump;
    public Integer climb;
    public Integer sports;
    public Integer diplomacy;
    public Integer bluff;
    public Integer intimidate;
    public Integer arcane;
    public Integer religion;
    public Integer nature;
    public Integer perceive;
    public Integer feeling;
    public Integer search;
    public Integer hide;
    public Integer stealth;
    public Integer acrobacy;
    public Integer wounds;
    public Integer toxicity;
    public Integer curse;
    public Integer attack;
    public Integer evasion;
    public Integer specialAttack;
    public Integer resistance;

    public static List<String> getList(){
        return Arrays.asList("openLocks", "steal", "operateTrap", "jump", "climb",
                "sports", "diplomacy", "bluff", "intimidate", "arcane","religion",
                "nature","perceive","feeling","search","hide","stealth","acrobacy",
                "wounds","toxicity","curse", "attack","evasion","specialAttack", "resistance");
    }
}
