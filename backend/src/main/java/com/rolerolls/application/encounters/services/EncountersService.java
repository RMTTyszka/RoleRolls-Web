package com.rolerolls.application.encounters.services;

import com.rolerolls.application.encounters.dtos.EncounterInput;
import com.rolerolls.domain.creatures.monsters.models.MonsterModel;
import com.rolerolls.domain.creatures.monsters.models.MonsterModelRepository;
import com.rolerolls.domain.encounters.Encounter;
import com.rolerolls.domain.encounters.EncounterRepository;
import com.rolerolls.shared.BaseCrudService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.UUID;
import java.util.stream.Collectors;

@Service
public class EncountersService extends BaseCrudService<Encounter, EncounterInput, EncounterInput, EncounterRepository> {

    @Autowired
    private MonsterModelRepository monsterModelRepository;

    public EncountersService(EncounterRepository repository) {
        super(repository);
    }

    @Override
    public EncounterInput getNew() {
        return new EncounterInput();
    }
    @Override
    public Encounter get(UUID id) {
        Encounter encounter =  this.repository.findById(id).get();
        List<MonsterModel> monsterTemplates = monsterModelRepository.findAllByIdIn(encounter.getMonsterTemplateIds());
        encounter.getMonsterTemplateIds().forEach(templateId -> {
            MonsterModel template = monsterTemplates.stream().filter(monsterTemplate -> monsterTemplate.getId().equals(templateId)).findFirst().get();
            encounter.getMonsters().add(template);
        });
        return encounter;
    }
    @Override
    protected Encounter createInputToEntity(EncounterInput encounterInput) {
        return new Encounter(encounterInput.getName(), encounterInput.getLevel(),
                encounterInput.getMonsters().stream().map(monsterModel -> monsterModel.getId()).collect(Collectors.toList()),
                encounterInput.getEnviroments());
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
