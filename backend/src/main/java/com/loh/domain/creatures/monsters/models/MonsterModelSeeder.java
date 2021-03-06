package com.loh.domain.creatures.monsters.models;

import com.loh.domain.races.Race;
import com.loh.domain.races.RaceRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class MonsterModelSeeder {

    @Autowired
    MonsterModelRepository monsterModelRepository;
    @Autowired
    RaceRepository raceRepository;

    public void seed() throws Exception {

        if (!monsterModelRepository.existsById(DefaultMonsterModelsMapping.models.get(DefaultMonsterModel.Zombie))) {
            Race undead = raceRepository.findByNameAndSystemDefaultTrue("Undead");
            MonsterModel model = new MonsterModel();
        }

    }
}

