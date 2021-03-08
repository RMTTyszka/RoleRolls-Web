package com.loh.domain.races.land.of.heroes;

import com.loh.domain.creatures.CreatureType;
import com.loh.domain.races.DefaultRace;
import com.loh.domain.races.Race;
import com.loh.domain.races.RaceRepository;
import com.loh.domain.universes.UniverseType;
import com.loh.shared.Bonus;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
public class LandOfHeroesHeroRaceSeeder {

    @Autowired
    RaceRepository raceRepository;

    public void seed() {
        for (DefaultRace defaultRace : LandOfHeroesDefaultHeroRaces.races) {
            Race race = raceRepository.findByNameAndUniverseTypeAndSystemDefaultTrue(defaultRace.name, UniverseType.TheFutureIsOutThere);
            if (race == null) {
                List<Bonus> bonuses = new ArrayList<>();
                for (Bonus bonus : defaultRace.bonuses) {
                    bonuses.add(bonus);
                }
                race = new Race(defaultRace.name, bonuses, null, null, CreatureType.Hero, UniverseType.TheFutureIsOutThere, true);
                raceRepository.save(race);
            } else {
                List<Bonus> bonuses = new ArrayList<>();
                for (Bonus bonus : defaultRace.bonuses) {
                    bonuses.add(bonus);
                }
                race.setBonuses(bonuses);
                raceRepository.save(race);
            }
        }
    }
}
