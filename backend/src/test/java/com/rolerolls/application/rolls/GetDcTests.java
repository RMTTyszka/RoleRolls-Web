package com.rolerolls.application.rolls;

import com.rolerolls.rolls.DcResult;
import com.rolerolls.rolls.RollDCService;
import org.junit.Test;
import org.springframework.boot.test.context.SpringBootTest;

import java.util.List;

@SpringBootTest
public class GetDcTests {


    RollDCService rollDCService = new RollDCService();

    @Test
    public void TestGetDC() {
        Integer points = 19;
        Integer bonus = 2;
        List<DcResult> result = rollDCService.getDC(points, bonus, 50d);
        for (DcResult dcResult : result) {
            System.out.println(String.format("%d / %d / %f", dcResult.difficulty, dcResult.complexity, rollDCService.verifyDC(points, bonus, dcResult.difficulty, dcResult.complexity)));
        }
    }

}
