package com.loh.creatures.monsters;

import java.util.Optional;

import javax.json.Json;
import javax.json.JsonObject;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/monsterBase",  produces = "application/json; charset=UTF-8")
public class MonsterBaseController {

	@Autowired
	private MonsterBaseRepository monsterBaseRepo;
	
	@GetMapping(path="/all")
	public @ResponseBody Iterable<MonsterBase> getAllMonsterBases() {
		
		// This returns a JSON or XML with the users
		return monsterBaseRepo.findAll();
	}
	@GetMapping(path="/find")
	public @ResponseBody Optional<MonsterBase> getMonsterById(@RequestParam Integer id) {
		// This returns a JSON or XML with the users
		return monsterBaseRepo.findById(id);
	}
	@PostMapping(path="/create")
	public @ResponseBody JsonObject createMonsterBase(@RequestBody MonsterBase monsterBaseDto) {
		// This returns a JSON or XML with the users
		monsterBaseRepo.save(monsterBaseDto);
		
		return Json.createObjectBuilder()
				.add("text", "race saved with success").build();
	}
	@PutMapping(path="/update")
	public @ResponseBody JsonObject updateMonsterBase(@RequestBody MonsterBase monsterBaseDto) {

		monsterBaseRepo.save(monsterBaseDto);
		
		return Json.createObjectBuilder()
				.add("text", "race updated with success").build();
	}
	
	@DeleteMapping(path="/delete")
	public @ResponseBody JsonObject deleteMonsterBase(@RequestBody Integer id) {
		
		monsterBaseRepo.deleteById(id);
		
		return Json.createObjectBuilder()
				.add("text", "race deleted with success").build();
	}
}
