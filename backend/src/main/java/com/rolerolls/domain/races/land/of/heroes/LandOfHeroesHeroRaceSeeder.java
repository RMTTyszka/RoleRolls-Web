package com.rolerolls.domain.races.land.of.heroes;

import com.rolerolls.domain.creatures.CreatureType;
import com.rolerolls.domain.races.DefaultRace;
import com.rolerolls.domain.races.Race;
import com.rolerolls.domain.races.RaceRepository;
import com.rolerolls.domain.universes.UniverseType;
import com.rolerolls.shared.Bonus;
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
            Race race = raceRepository.findByNameAndUniverseTypeAndSystemDefaultTrue(defaultRace.name, UniverseType.LandOfHeroes);
            if (race == null) {
                List<Bonus> bonuses = new ArrayList<>();
                for (Bonus bonus : defaultRace.bonuses) {
                    bonuses.add(bonus);
                }
                race = new Race(defaultRace.name, bonuses, null, null, CreatureType.Hero, UniverseType.LandOfHeroes, true);
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
