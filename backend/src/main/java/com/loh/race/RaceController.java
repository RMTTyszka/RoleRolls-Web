package com.loh.race;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import javax.json.Json;
import javax.json.JsonObject;
import java.util.Optional;
import java.util.UUID;
import java.util.stream.Collectors;


@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/races",  produces = "application/json; charset=UTF-8") // This means URL's start with /demo (after Application path)
public class RaceController {

	@Autowired
	private RaceRepository repository;


	@GetMapping(path="/allPaged")
	public @ResponseBody
	Iterable<Race> getAllPaged(@RequestParam String filter, @RequestParam int skipCount, @RequestParam int maxResultCount) {

		Pageable paged = PageRequest.of(skipCount, maxResultCount);
		if (filter.isEmpty() || filter == null) {
			return repository.findAll(paged);
		}
		return repository.findAllByNameIgnoreCaseContaining(filter, paged);
	}
	@GetMapping(path="/allFiltered")
	public @ResponseBody
	Iterable<Race> getAllFiltered(@RequestParam String filter) {
		// This returns a JSON or XML with the users
		if (filter.isEmpty() || filter == null) {
			return repository.findAll();
		}
		return repository.findAllByNameIgnoreCaseContaining(filter);
	}
	@GetMapping(path="/find")
	public @ResponseBody Optional<Race> getOneRaces(@RequestParam UUID id) {
		// This returns a JSON or XML with the users
		return repository.findById(id);
	}
	@PostMapping(path="/create")
	public @ResponseBody JsonObject createRace(@RequestBody Race raceDto) {
		// This returns a JSON or XML with the users
		repository.save(raceDto);
		
		return Json.createObjectBuilder()
				.add("text", "race saved with success").build();
	}
	@PutMapping(path="/update")
	public @ResponseBody JsonObject updateRace(@RequestBody Race raceDto) {
		System.out.println(raceDto.getBonuses().stream().map(b -> b.getProperty()).collect(Collectors.toList()));

		repository.save(raceDto);
		
		return Json.createObjectBuilder()
				.add("text", "race updated with success").build();
	}
	
	@DeleteMapping(path="/delete")
	public @ResponseBody JsonObject deleteRace(@RequestParam UUID id) {

		repository.deleteById(id);
		
		return Json.createObjectBuilder()
				.add("text", "race deleted with success").build();
	}
	

}
