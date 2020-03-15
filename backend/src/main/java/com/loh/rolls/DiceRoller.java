package com.loh.rolls;

import com.loh.dev.Loh;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

@Service
public class DiceRoller {

    private Random random = new Random();

    public RollResult makeTest(Integer level, Integer bonus, Integer difficulty, Integer complexity) {

        Integer numberOfRolls = Loh.getLevel(level);
        List<Integer> rolls = new ArrayList<>();
        Integer criticalSuccesses = 0;
        Integer criticalFailures = 0;
        Integer rollSuccesses = 0;
        Integer bonusDice = getBonusDiceRoll(level);
        boolean bonusDiceConsumed = false;

        for (int i = 0; i < numberOfRolls; i++) {
            Integer roll = getRoll(20);
            rolls.add(roll);
            if (roll == 20) {
                rollSuccesses++;
                criticalSuccesses++;
            }
            else if (roll == 1) {
                criticalFailures++;
            }
            else if (roll + bonus >= difficulty) {
                rollSuccesses++;
            }
            else if (!bonusDiceConsumed && roll + bonus + bonusDice >= difficulty) {
                rollSuccesses++;
                bonusDiceConsumed = true;
            }
        }
        boolean success = rollSuccesses >= complexity;
        Integer successes = rollSuccesses / complexity;

        return new RollResult(success, rolls, bonusDice, rollSuccesses, criticalSuccesses, criticalFailures, successes, difficulty, complexity);
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
