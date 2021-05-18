package com.rolerolls.application.encounters;

import com.rolerolls.application.encounters.dtos.EncounterInput;
import com.rolerolls.domain.encounters.Encounter;
import com.rolerolls.domain.encounters.EncounterRepository;
import com.rolerolls.shared.BaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/encounters",  produces = "application/json; charset=UTF-8")
public class EncounterController extends BaseCrudController<Encounter, EncounterInput, EncounterInput, EncounterRepository> {

	@Autowired
	private EncounterRepository encounterRepository;

	public EncounterController(EncounterRepository repository) {
		super(repository);
	}

	@GetMapping(path="/all")
	public @ResponseBody Iterable<Encounter> getAllEncounters() {
		
		// This returns a JSON or XML with the users
		return encounterRepository.findAll();
	}

	@Override
	public EncounterInput getNew() {
		return new EncounterInput();
	}

	@Override
	protected Encounter createInputToEntity(EncounterInput encounterInput) {
		return new Encounter(encounterInput.getLevel(), encounterInput.getMonsters(), encounterInput.getEnviroments());
	}

	@Override
	protected Encounter updateInputToEntity(EncounterInput encounterInput) {
		return new Encounter(encounterInput.getLevel(), encounterInput.getMonsters(), encounterInput.getEnviroments());
	}
}
