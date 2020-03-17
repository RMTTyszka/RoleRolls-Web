package com.loh.tests.WeaponTest;

import com.loh.combat.AttackService;
import com.loh.creatures.heroes.HeroRepository;
import org.springframework.beans.factory.annotation.Autowired;

public class WeaponTest {

    @Autowired
    private HeroRepository heroRepository;
    @Autowired
    private AttackService attackService;

}
