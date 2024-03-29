package com.rolerolls.application.adventures;

import com.rolerolls.domain.adventures.Adventure;
import com.rolerolls.domain.adventures.AdventureRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/adventures",  produces = "application/json; charset=UTF-8")
public class AdventureController {

	@Autowired
	private AdventureRepository adventureRepository;
	
	@GetMapping(path="/all")
	public @ResponseBody Iterable<Adventure> getAllAdventures() {
		
		// This returns a JSON or XML with the users
		return adventureRepository.findAll();
	}
}
