package com.loh.rolls;

import com.loh.system.Loh;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

public class DiceRoller {

    private Random random = new Random();

    public RollTestResult roll(Integer points, Integer bonus) {

        Integer numberOfRolls = Loh.getLevel(points);
        List<Roll> rolls = new ArrayList<>();
        Integer criticalSuccesses = 0;
        Integer criticalFailures = 0;
        Integer rollSuccesses = 0;
        Integer bonusDice = getBonusDiceRoll(points);
        boolean bonusDiceConsumed = false;

        for (int i = 0; i < numberOfRolls; i++) {
            Integer diceRoll = getRoll(20);
            Roll roll = new Roll(diceRoll, bonus, false);
            rolls.add(roll);
        }

        return new RollTestResult(true, numberOfRolls, rolls, bonusDice, rollSuccesses, criticalSuccesses, criticalFailures, null, null, null);
    }

    public RollTestResult rollTest(Integer points, Integer bonus, Integer difficulty, Integer complexity) {

        Integer numberOfRolls = Loh.getLevel(points);
        List<Roll> rolls = new ArrayList<>();
        Integer criticalSuccesses = 0;
        Integer criticalFailures = 0;
        Integer rollSuccesses = 0;
        Integer bonusDice = getBonusDiceRoll(points);
        boolean bonusDiceConsumed = false;

        for (int i = 0; i < numberOfRolls; i++) {
            Integer diceRoll = getRoll(20);
            if (diceRoll == 20) {
                rollSuccesses++;
                criticalSuccesses++;
                Roll roll = new Roll(diceRoll, bonus, true);
                rolls.add(roll);
            }
            else if (diceRoll == 1) {
                criticalFailures++;
                Roll roll = new Roll(diceRoll, bonus, false);
                rolls.add(roll);
            }
            else if (diceRoll + bonus >= difficulty) {
                rollSuccesses++;
                Roll roll = new Roll(diceRoll, bonus, true);
                rolls.add(roll);
            }
            else if (!bonusDiceConsumed && diceRoll + bonus + bonusDice >= difficulty) {
                rollSuccesses++;
                bonusDiceConsumed = true;
                rollSuccesses++;
                Roll roll = new Roll(diceRoll, bonus, true, bonusDice);
                rolls.add(roll);
            } else {
                Roll roll = new Roll(diceRoll, bonus, false);
                rolls.add(roll);
            }
        }
        boolean success = rollSuccesses >= complexity;
        Integer successes = rollSuccesses / complexity;

        return new RollTestResult(success, numberOfRolls, rolls, bonusDice, rollSuccesses, criticalSuccesses, criticalFailures, successes, difficulty, complexity);
    }

    public Integer getBonusDice(Integer level) {
        return (level + 5) % 5 * 4;
    }

    public Integer getRoll(Integer dice) {
        return dice == 0 ? 0 : random.nextInt(dice) + 1;
    }

    public Integer getBonusDiceRoll(Integer level) {
        Integer bonusDice = getBonusDice(level);
        boolean levelHasNoBonusDice = bonusDice == 0;
        return levelHasNoBonusDice ? 0 : getRoll(bonusDice);
    }

}
