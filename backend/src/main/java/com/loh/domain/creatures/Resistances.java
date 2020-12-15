package com.loh.domain.creatures;

import lombok.Getter;

public class Resistances {
    @Getter
    private Integer fear;
    @Getter
    private Integer health;
    @Getter
    private Integer magic;
    @Getter
    private Integer physical;
    @Getter
    private Integer reflex;

    public Resistances(Integer fear, Integer health, Integer magic, Integer physical, Integer reflex) {
        this.fear = fear;
        this.health = health;
        this.magic = magic;
        this.physical = physical;
        this.reflex = reflex;
    }

    static String Health = "Health Resistance";
    static String Physical = "Physical Resistance";
    static String Magic = "Magic Resistance";
    static String Fear = "Fear Resistance";
    static String Reflex = "Reflex Resistance";


}
