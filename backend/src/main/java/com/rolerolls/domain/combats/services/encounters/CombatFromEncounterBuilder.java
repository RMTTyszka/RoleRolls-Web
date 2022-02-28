package com.rolerolls.domain.combats.services.encounters;

import com.rolerolls.domain.combats.Combat;
import com.rolerolls.domain.combats.CombatRepository;
import com.rolerolls.domain.creatures.monsters.Monster;
import com.rolerolls.domain.creatures.monsters.MonsterRepository;
import com.rolerolls.domain.creatures.monsters.services.MonsterInstantiatorService;
import com.rolerolls.domain.encounters.Encounter;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

@Service
public class CombatFromEncounterBuilder {

    @Autowired
    public MonsterInstantiatorService monsterInstantiatorService;
    @Autowired
    public MonsterRepository monsterRepository;
    @Autowired
    private CombatRepository combatRepository;
    public Combat BuildFromEncounter(UUID campaignId, Encounter encounter) {
        Combat combat = new Combat();
        List<Monster> monsters = encounter.getMonsters()
                .stream()
                .map(monsterTemplate -> monsterInstantiatorService.Instantiate(monsterTemplate, encounter.getLevel()))
                .collect(Collectors.toList());
        monsters.forEach(monster -> {
            monster = monsterRepository.save(monster);
        });
        combat.setMonsters(monsters);

        combat.setCampaignId(campaignId);
        combat = combatRepository.save(combat);
        return combat;
    }
}
