package com.loh.creatures;

import com.loh.creatures.heroes.HeroSeeder;
import com.loh.creatures.monsters.MonsterSeeder;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/creature",  produces = "application/json; charset=UTF-8")
public class CreatureController {
    @Autowired
    private CreatureRepository creatureRepository;
    @Autowired
    private HeroSeeder heroSeeder;
    @Autowired
    private MonsterSeeder monsterSeeder;

    @Transactional
    @PostMapping(path="/deleteDummies")
    public @ResponseBody
    void deleteDummies() throws Exception {
        creatureRepository.deleteByNameContaining("Dummy");
        heroSeeder.seed();
        monsterSeeder.seed();

    }
}
