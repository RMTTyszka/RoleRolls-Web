package com.loh.rolls;

import lombok.Getter;

import java.util.List;

public class RollResult {

    @Getter
    private Integer successes;
    @Getter
    private List<Integer> rolls;

    @Getter
    private Integer bonusDice;

    RollResult(Integer successes, List<Integer> rolls, Integer bonusDice) {
        this.successes = successes;
        this.rolls = rolls;
        this.bonusDice = bonusDice;
    }
}
