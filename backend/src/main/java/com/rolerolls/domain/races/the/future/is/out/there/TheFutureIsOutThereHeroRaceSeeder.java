package com.rolerolls.domain.races.the.future.is.out.there;

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
