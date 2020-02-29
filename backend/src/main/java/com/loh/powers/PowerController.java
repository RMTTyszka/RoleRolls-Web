package com.loh.powers;

import java.util.List;
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

import com.loh.race.Race;
import com.loh.race.RaceRepository;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/powers",  produces = "application/json; charset=UTF-8") // This means URL's start with /demo (after Application path)
public class PowerController {
	
	@Autowired
	private PowerRepository powerRepository;
	@Autowired
	private RaceRepository raceRepository;
	
	private Grimory grimory;
	
	@GetMapping(path="/all")
	public @ResponseBody Iterable<Power> getAllPowers() {
		
		// This returns a JSON or XML with the users
		return powerRepository.findAll();
	}
	@GetMapping(path="/find")
	public @ResponseBody Optional<Power> getPower(@RequestParam Integer id) {
		// This returns a JSON or XML with the users
		return powerRepository.findById(id);
	}
	@PostMapping(path="/create")
	public @ResponseBody JsonObject createPower(@RequestBody Power PowerDto) {
		// This returns a JSON or XML with the users
		powerRepository.save(PowerDto);
		
		return Json.createObjectBuilder()
				.add("text", "Power saved with success").build();
	}
	@PutMapping(path="/update")
	public @ResponseBody JsonObject updatePower(@RequestBody Power PowerDto) {

		powerRepository.save(PowerDto);
		
		return Json.createObjectBuilder()
				.add("text", "Power updated with success").build();
	}
	
	@DeleteMapping(path="/delete")
	public @ResponseBody JsonObject deletePower(@RequestParam Integer id) {
		
		powerRepository.deleteById(id);
		
		return Json.createObjectBuilder()
				.add("text", "Power deleted with success").build();
	}
	@GetMapping(path="/findPowerUsages")
	public @ResponseBody Iterable<Race> getPowerUsages(@RequestParam Integer id) {
		// This returns a JSON or XML with the users

		return raceRepository.findByPowersId(id);
	}
}
