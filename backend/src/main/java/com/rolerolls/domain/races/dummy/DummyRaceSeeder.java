package com.rolerolls.domain.races.dummy;

import com.rolerolls.domain.creatures.CreatureType;
import com.rolerolls.domain.races.Race;
import com.rolerolls.domain.races.RaceRepository;
import com.rolerolls.domain.universes.UniverseType;
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
