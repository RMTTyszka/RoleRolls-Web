package com.loh.role;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import javax.json.Json;
import javax.json.JsonObject;
import java.util.UUID;


@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/roles",  produces = "application/json; charset=UTF-8")
public class RoleController {

	@Autowired
	private RoleRepository repository;


	@GetMapping(path="/allPaged")
	public @ResponseBody
	Iterable<Role> getAllPaged(@RequestParam String filter, @RequestParam int skipCount, @RequestParam int maxResultCount) {

		Pageable paged = PageRequest.of(skipCount, maxResultCount);
		if (filter.isEmpty() || filter == null) {
			return repository.findAll(paged);
		}
		return repository.findAllByNameIgnoreCaseContaining(filter, paged);
	}
	@GetMapping(path="/allFiltered")
	public @ResponseBody
	Iterable<Role> getAllFiltered(@RequestParam String filter) {
		// This returns a JSON or XML with the users
		if (filter.isEmpty() || filter == null) {
			return repository.findAll();
		}
		return repository.findAllByNameIgnoreCaseContaining(filter);
	}
	@GetMapping(path="/find")
	public @ResponseBody Role getRole(UUID id) {

		// This returns a JSON or XML with the users
		return repository.findById(id).get();
	}

	@PostMapping(path="/create")
	public @ResponseBody JsonObject createRace(@RequestBody Role roleDto) {
		// This returns a JSON or XML with the users
		repository.save(roleDto);
		
		return Json.createObjectBuilder()
				.add("text", "role saved with success").build();
	}
	
	@PutMapping(path="/update")
	public @ResponseBody JsonObject updateRace(@RequestBody Role roleDto) {

		repository.save(roleDto);
		
		return Json.createObjectBuilder()
				.add("text", "role updated with success").build();
	}
	@DeleteMapping(path="/delete")
	public @ResponseBody JsonObject deleteRace(@RequestParam UUID id) {

		repository.deleteById(id);
		
		return Json.createObjectBuilder()
				.add("text", "role deleted with success").build();
	}
	
	
}
