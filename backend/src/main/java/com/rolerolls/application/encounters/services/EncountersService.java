package com.rolerolls.application.encounters.services;

import com.rolerolls.application.encounters.dtos.EncounterInput;
import com.rolerolls.domain.creatures.monsters.models.MonsterModel;
import com.rolerolls.domain.encounters.Encounter;
import com.rolerolls.domain.encounters.EncounterRepository;
import com.rolerolls.shared.BaseCrudService;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class EncountersService extends BaseCrudService<Encounter, EncounterInput, EncounterInput, EncounterRepository> {

    public EncountersService(EncounterRepository repository) {
        super(repository);
    }

    @Override
    public EncounterInput getNew() {
        return new EncounterInput();
    }

    @Override
    protected Encounter createInputToEntity(EncounterInput encounterInput) {
        return new Encounter(encounterInput.getName(), encounterInput.getLevel(), encounterInput.getMonsters(), encounterInput.getEnviroments());
    }

    @Override
    protected Encounter updateInputToEntity(UUID id, EncounterInput encounterInput) {
        Encounter encounter = repository.findById(id).get();
        encounter.setName(encounterInput.getName());
        encounter.setLevel(encounterInput.getLevel());
        return encounter;
    }
    public ResponseEntity<?> addMonsterTemplate(UUID encounterId, MonsterModel monsterTemplate) {
        Encounter encounter = repository.findById(encounterId).get();
        Boolean success = encounter.addMonsterTemplate(monsterTemplate);
        if (success) {
            repository.save(encounter);
            return ResponseEntity.ok().build();
        } else {
            return ResponseEntity.unprocessableEntity().build();
        }

    }
    public ResponseEntity<?> removeMonsterTemplate(UUID encounterId, UUID monsterTemplateId) {
        Encounter encounter = repository.findById(encounterId).get();
        encounter.removeMonsterTemplate(monsterTemplateId);
        repository.save(encounter);
        return ResponseEntity.ok().build();
    }
}
