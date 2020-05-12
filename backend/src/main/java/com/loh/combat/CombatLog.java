package com.loh.combat;

import java.io.Serializable;

public class CombatLog implements Serializable {
    public String log;

    public CombatLog(String log) {
        this.log = log;
    }
}
