package com.loh.rolls;

import lombok.Getter;

import java.util.List;

public class RollResult {

    @Getter
    private boolean success;
    @Getter
    private List<Integer> rolls;
    @Getter
    private Integer bonusDice;
    @Getter
    private Integer successes;
    @Getter
    private Integer criticalSuccesses;
    @Getter
    private Integer criticalFailures;
    @Getter
    private Integer difficulty;
    @Getter
    private Integer complexity;

    public RollResult(boolean success, List<Integer> rolls, Integer bonusDice, Integer successes, Integer criticalSuccesses, Integer criticalFailures, Integer difficulty, Integer complexity) {
        this.success = success;
        this.rolls = rolls;
        this.bonusDice = bonusDice;
        this.successes = successes;
        this.criticalSuccesses = criticalSuccesses;
        this.criticalFailures = criticalFailures;
        this.difficulty = difficulty;
        this.complexity = complexity;
    }
}
