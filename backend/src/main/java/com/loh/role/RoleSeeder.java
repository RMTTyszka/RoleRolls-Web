package com.loh.role;

import com.loh.creatures.Attributes;
import com.loh.shared.Bonus;
import com.loh.shared.BonusType;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
public class RoleSeeder {

    @Autowired
    RoleRepository roleRepository;

    public void seed() {
        if (roleRepository.findByNameAndSystemDefaultTrue("Warrior") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Strength, 2, 0, BonusType.Innate));
            Role role = new Role("Warrior", bonuses, null);
            roleRepository.save(role);
        }
        if (roleRepository.findByNameAndSystemDefaultTrue("Wizard") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Wisdom, 2, 0, BonusType.Innate));
            Role role = new Role("Wizard", bonuses, null);
            roleRepository.save(role);
        }
        if (roleRepository.findByNameAndSystemDefaultTrue("Rogue") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Agility, 2, 0, BonusType.Innate));
            Role role = new Role("Rogue", bonuses, null);
            roleRepository.save(role);
        }
        if (roleRepository.findByNameAndSystemDefaultTrue("Cleric") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Intuition, 2, 0, BonusType.Innate));
            Role role = new Role("Cleric", bonuses, null);
            roleRepository.save(role);
        }
        if (roleRepository.findByNameAndSystemDefaultTrue("Paladin") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Intuition, 2, 0, BonusType.Innate));
            Role role = new Role("Paladin", bonuses, null);
            roleRepository.save(role);
        }
        if (roleRepository.findByNameAndSystemDefaultTrue("Bard") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Charisma, 2, 0, BonusType.Innate));
            Role role = new Role("Bard", bonuses, null);
            roleRepository.save(role);
        }
        if (roleRepository.findByNameAndSystemDefaultTrue("Druid") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Vitality, 2, 0, BonusType.Innate));
            Role role = new Role("Druid", bonuses, null);
            roleRepository.save(role);
        }
        if (roleRepository.findByNameAndSystemDefaultTrue("Hunter") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Agility, 2, 0, BonusType.Innate));
            Role role = new Role("Hunter", bonuses, null);
            roleRepository.save(role);
        }
        if (roleRepository.findByNameAndSystemDefaultTrue("Barbarian") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Strength, 2, 0, BonusType.Innate));
            Role role = new Role("Barbarian", bonuses, null);
            roleRepository.save(role);
        }
        if (roleRepository.findByNameAndSystemDefaultTrue("Monk") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Vitality, 2, 0, BonusType.Innate));
            Role role = new Role("Monk", bonuses, null);
            roleRepository.save(role);
        }
        if (roleRepository.findByNameAndSystemDefaultTrue("Warlord") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Charisma, 2, 0, BonusType.Innate));
            Role role = new Role("Warlord", bonuses, null);
            roleRepository.save(role);
        }
        if (roleRepository.findByNameAndSystemDefaultTrue("Witchdoctor") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            bonuses.add(new Bonus(Attributes.Wisdom, 2, 0, BonusType.Innate));
            Role role = new Role("Witchdoctor", bonuses, null);
            roleRepository.save(role);
        }
        if (roleRepository.findByNameAndSystemDefaultTrue("Dummy") == null) {
            List<Bonus> bonuses = new ArrayList<>();
            Role role = new Role("Dummy", bonuses, null);
            roleRepository.save(role);
        }
    }
}
