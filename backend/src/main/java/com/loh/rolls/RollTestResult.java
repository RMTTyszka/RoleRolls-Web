package com.loh.rolls;

import lombok.Getter;

import java.util.List;

public class RollTestResult {

    @Getter
    private boolean success;
    @Getter
    private List<Roll> rolls;

    @Getter
    private Integer bonusDice;
    @Getter
    private Integer numberOfRolls;
    @Getter
    private Integer rollSuccesses;
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

    public RollTestResult(boolean success, Integer numberOfRolls, List<Roll> rolls, Integer bonusDice, Integer rollSuccesses, Integer criticalSuccesses, Integer criticalFailures, Integer successes, Integer difficulty, Integer complexity) {
        this.success = success;
        this.numberOfRolls = numberOfRolls;
        this.rolls = rolls;
        this.bonusDice = bonusDice;
        this.rollSuccesses = rollSuccesses;
        this.successes = successes;
        this.criticalSuccesses = criticalSuccesses;
        this.criticalFailures = criticalFailures;
        this.difficulty = difficulty;
        this.complexity = complexity;
    }
}
