package com.rolerolls.domain.adventures;

import com.rolerolls.application.adventures.encounters.Encounter;
import com.rolerolls.application.adventures.encounters.EncounterRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/encounters",  produces = "application/json; charset=UTF-8")
public class EncounterController {

	@Autowired
	private EncounterRepository encounterRepository;
	
	@GetMapping(path="/all")
	public @ResponseBody Iterable<Encounter> getAllEncounters() {
		
		// This returns a JSON or XML with the users
		return encounterRepository.findAll();
	}
}
