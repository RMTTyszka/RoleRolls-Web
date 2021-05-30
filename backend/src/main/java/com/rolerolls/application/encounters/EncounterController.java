package com.rolerolls.application.encounters;

import com.rolerolls.application.encounters.dtos.EncounterInput;
import com.rolerolls.domain.creatures.monsters.models.MonsterModel;
import com.rolerolls.domain.encounters.Encounter;
import com.rolerolls.domain.encounters.EncounterRepository;
import com.rolerolls.shared.BaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/encounters",  produces = "application/json; charset=UTF-8")
public class EncounterController extends BaseCrudController<Encounter, EncounterInput, EncounterInput, EncounterRepository> {

	@Autowired
	private EncounterRepository encounterRepository;

	public EncounterController(EncounterRepository repository) {
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

	@PostMapping(path="/{encounterId}/monsters")
	public ResponseEntity<?> addMonsterTemplate(@PathVariable UUID encounterId, @RequestBody MonsterModel monsterTemplate) {
		Encounter encounter = repository.findById(encounterId).get();
		Boolean success = encounter.addMonsterTemplate(monsterTemplate);
		if (success) {
			repository.save(encounter);
			return ResponseEntity.ok().build();
		} else {
			return ResponseEntity.unprocessableEntity().build();
		}

	}
	@DeleteMapping(path="/{encounterId}/monsters/{monsterTemplateId}")
	public ResponseEntity<?> addMonsterTemplate(@PathVariable UUID encounterId, @PathVariable UUID monsterTemplateId) {
		Encounter encounter = repository.findById(encounterId).get();
		encounter.removeMonsterTemplate(monsterTemplateId);
		repository.save(encounter);
		return ResponseEntity.ok().build();

	}
}
