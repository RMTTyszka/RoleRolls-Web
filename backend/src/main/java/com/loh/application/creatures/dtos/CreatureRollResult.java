package com.loh.application.creatures.dtos;

import com.loh.rolls.Roll;
import lombok.Getter;

import java.util.List;
import java.util.UUID;

public class CreatureRollResult {

    @Getter
    private UUID creatureId;
    @Getter
    private String creatureName;
    private String property;
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

    public CreatureRollResult(UUID creatureId, String creatureName, String property, boolean success, List<Roll> rolls, Integer bonusDice, Integer numberOfRolls, Integer rollSuccesses, Integer successes, Integer criticalSuccesses, Integer criticalFailures, Integer difficulty, Integer complexity) {
        this.creatureId = creatureId;
        this.creatureName = creatureName;
        this.property = property;
        this.success = success;
        this.rolls = rolls;
        this.bonusDice = bonusDice;
        this.numberOfRolls = numberOfRolls;
        this.rollSuccesses = rollSuccesses;
        this.successes = successes;
        this.criticalSuccesses = criticalSuccesses;
        this.criticalFailures = criticalFailures;
        this.difficulty = difficulty;
        this.complexity = complexity;
    }
}
