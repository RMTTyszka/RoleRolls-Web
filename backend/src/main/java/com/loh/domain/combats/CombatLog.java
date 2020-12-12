package com.loh.domain.combats;

import java.io.Serializable;

public class CombatLog implements Serializable {
    public String log;

    public CombatLog(String log) {
        this.log = log;
    }
}
