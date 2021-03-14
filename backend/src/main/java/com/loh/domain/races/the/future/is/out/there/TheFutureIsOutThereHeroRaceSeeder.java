package com.loh.domain.races.the.future.is.out.there;

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
public class TheFutureIsOutThereHeroRaceSeeder {

    @Autowired
    RaceRepository raceRepository;

    public void seed() {
        for (DefaultRace defaultRace : TheFutureIsOutThereDefaultRaces.races) {
            if (raceRepository.findByNameAndUniverseTypeAndSystemDefaultTrue(defaultRace.name, UniverseType.TheFutureIsOutThere) == null) {
                List<Bonus> bonuses = new ArrayList<>();
                for (Bonus bonus : defaultRace.bonuses) {
                    bonuses.add(bonus);
                }
                Race race = new Race(defaultRace.name, bonuses, null, null, CreatureType.Hero, UniverseType.TheFutureIsOutThere, true);
                raceRepository.save(race);
            }
        }
    }
}
