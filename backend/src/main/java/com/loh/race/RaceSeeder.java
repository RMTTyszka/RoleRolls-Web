package com.loh.race;

import com.loh.creatures.Attributes;
import com.loh.shared.Bonus;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
public class RaceSeeder {

    @Autowired
    RaceRepository raceRepository;

    public void seed() {
        if (raceRepository.findByNameAndSystemDefaultTrue("Elf") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Agility, 8));
            bonuses.add(new Bonus(Attributes.Vitality, -2));
            Race elf = new Race("Elf", bonuses, null, null);
            raceRepository.save(elf);
        }
        if (raceRepository.findByNameAndSystemDefaultTrue("Dwarf") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Vitality, 8));
            bonuses.add(new Bonus(Attributes.Charisma, -2));
            Race dwarf = new Race("Dwarf", bonuses, null, null);
            raceRepository.save(dwarf);
        }
        if (raceRepository.findByNameAndSystemDefaultTrue("Orc") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Strength, 8));
            bonuses.add(new Bonus(Attributes.Wisdom, -2));
            Race dwarf = new Race("Orc", bonuses, null, null);
            raceRepository.save(dwarf);
        }
        if (raceRepository.findByNameAndSystemDefaultTrue("Gnome") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Wisdom, 8));
            bonuses.add(new Bonus(Attributes.Strength, -2));
            Race race = new Race("Gnome", bonuses, null, null);
            raceRepository.save(race);
        }
        if (raceRepository.findByNameAndSystemDefaultTrue("Draekar") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Strength, 4));
            bonuses.add(new Bonus(Attributes.Vitality, 4));
            bonuses.add(new Bonus(Attributes.Intuition, -2));
            Race race = new Race("Draekar", bonuses, null, null);
            raceRepository.save(race);
        }
        if (raceRepository.findByNameAndSystemDefaultTrue("Daennan") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Wisdom, 4));
            bonuses.add(new Bonus(Attributes.Intuition, 4));
            bonuses.add(new Bonus(Attributes.Vitality, -2));
            Race race = new Race("Daennan", bonuses, null, null);
            raceRepository.save(race);
        }
        if (raceRepository.findByNameAndSystemDefaultTrue("Rhassin") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Agility, 4));
            bonuses.add(new Bonus(Attributes.Vitality, 4));
            bonuses.add(new Bonus(Attributes.Wisdom, -2));
            Race race = new Race("Rhassin", bonuses, null, null);
            raceRepository.save(race);
        }
        if (raceRepository.findByNameAndSystemDefaultTrue("Goblin") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Wisdom, 4));
            bonuses.add(new Bonus(Attributes.Charisma, 4));
            bonuses.add(new Bonus(Attributes.Strength, -2));
            Race race = new Race("Goblin", bonuses, null, null);
            raceRepository.save(race);
        }
        if (raceRepository.findByNameAndSystemDefaultTrue("Halfling") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Agility, 4));
            bonuses.add(new Bonus(Attributes.Vitality, 4));
            bonuses.add(new Bonus(Attributes.Strength, -2));
            Race race = new Race("Halfling", bonuses, null, null);
            raceRepository.save(race);
        }
        if (raceRepository.findByNameAndSystemDefaultTrue("Ku-Toa") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Wisdom, 4));
            bonuses.add(new Bonus(Attributes.Strength, 4));
            bonuses.add(new Bonus(Attributes.Agility, -2));
            Race race = new Race("Ku-Toa", bonuses, null, null);
            raceRepository.save(race);
        }
        if (raceRepository.findByNameAndSystemDefaultTrue("Trolling") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Intuition, 4));
            bonuses.add(new Bonus(Attributes.Vitality, 4));
            bonuses.add(new Bonus(Attributes.Wisdom, -2));
            Race race = new Race("Trolling", bonuses, null, null);
            raceRepository.save(race);
        }
        if (raceRepository.findByNameAndSystemDefaultTrue("Tharian") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Intuition, 8));
            bonuses.add(new Bonus(Attributes.Vitality, -2));
            Race race = new Race("Tharian", bonuses, null, null);
            raceRepository.save(race);
        }
        if (raceRepository.findByNameAndSystemDefaultTrue("Human") == null) {
            Race elf = new Race("Human", null, null, null);
            raceRepository.save(elf);
        }
        if (raceRepository.findByNameAndSystemDefaultTrue("Dummy") == null) {
            Race race = new Race("Dummy", null, null, null);
            raceRepository.save(race);
        }
    }
}
