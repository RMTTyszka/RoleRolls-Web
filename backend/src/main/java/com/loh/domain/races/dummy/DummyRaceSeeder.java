package com.loh.domain.races.dummy;

import com.loh.domain.creatures.CreatureType;
import com.loh.domain.races.Race;
import com.loh.domain.races.RaceRepository;
import com.loh.domain.universes.UniverseType;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class DummyRaceSeeder {

    @Autowired
    RaceRepository raceRepository;

    public void seed() {
        if (raceRepository.findByNameAndUniverseTypeAndSystemDefaultTrue("Dummy", UniverseType.Dummy) == null) {
            Race race = new Race("Dummy", null, null, null, CreatureType.Dummy, UniverseType.Dummy, true);
            raceRepository.save(race);
        }
    }
}
