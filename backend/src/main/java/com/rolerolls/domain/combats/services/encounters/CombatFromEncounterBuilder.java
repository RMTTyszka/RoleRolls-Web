package com.rolerolls.domain.combats.services.encounters;

import com.rolerolls.domain.combats.Combat;
import com.rolerolls.domain.creatures.monsters.Monster;
import com.rolerolls.domain.creatures.monsters.services.MonsterInstantiatorService;
import com.rolerolls.domain.encounters.Encounter;
import org.springframework.beans.factory.annotation.Autowired;

import java.util.List;
import java.util.stream.Collectors;

public class CombatFromEncounterBuilder {

    @Autowired
    public MonsterInstantiatorService monsterInstantiatorService;

    public Combat BuildFromEncounter(Encounter encounter) {
        Combat combat = new Combat();
        List<Monster> monsters = encounter.getMonsters()
                .stream()
                .map(monsterTemplate -> monsterInstantiatorService.Instantiate(monsterTemplate, encounter.getLevel()))
                .collect(Collectors.toList());
        combat.setMonsters(monsters);

        return combat;
    }
}
