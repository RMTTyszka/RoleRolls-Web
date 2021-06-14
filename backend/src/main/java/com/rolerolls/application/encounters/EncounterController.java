package com.rolerolls.application.encounters;

import com.rolerolls.application.encounters.dtos.EncounterInput;
import com.rolerolls.application.encounters.services.EncountersService;
import com.rolerolls.domain.creatures.monsters.models.MonsterModel;
import com.rolerolls.domain.encounters.Encounter;
import com.rolerolls.domain.encounters.EncounterRepository;
import com.rolerolls.shared.BaseCrudControllerWithService;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/encounters",  produces = "application/json; charset=UTF-8")
public class EncounterController extends BaseCrudControllerWithService<Encounter, EncounterInput, EncounterInput, EncounterRepository, EncountersService> {

	public EncountersService service;
	public EncounterController(EncountersService service) {
		super(service);
		this.service = service;
	}

	@Override
	public EncounterInput getNew() {
		return this.service.getNew();
	}

	@PostMapping(path="/{encounterId}/monsters")
	public ResponseEntity<?> addMonsterTemplate(@PathVariable UUID encounterId, @RequestBody MonsterModel monsterTemplate) {
		return this.service.addMonsterTemplate(encounterId, monsterTemplate);
	}
	@DeleteMapping(path="/{encounterId}/monsters/{monsterTemplateId}")
	public ResponseEntity<?> removeMonsterTemplate(@PathVariable UUID encounterId, @PathVariable UUID monsterTemplateId) {
		return this.service.removeMonsterTemplate(encounterId, monsterTemplateId);
	}
}
