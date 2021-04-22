package com.rolerolls.application.shops;

import com.rolerolls.domain.shops.Shop;
import com.rolerolls.domain.shops.ShopRepository;
import com.rolerolls.domain.shops.ShopTokens;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import javax.json.Json;
import javax.json.JsonObject;
import java.util.Optional;
import java.util.UUID;


@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/shop",  produces = "application/json; charset=UTF-8") // This means URL's start with /demo (after Application path)
public class ShopController {

	@Autowired
	private ShopRepository repository;


	@GetMapping(path="/all")
	public @ResponseBody
	Iterable<Shop> getAll() {
		return repository.findAll();
	}

	@GetMapping(path="/allPaged")
	public @ResponseBody
	Iterable<Shop> getAllPaged(@RequestParam String filter, @RequestParam int skipCount, @RequestParam int maxResultCount) {

		Pageable paged = PageRequest.of(skipCount, maxResultCount);
		if (filter.isEmpty() || filter == null) {
			return repository.findAll(paged);
		}
		return repository.findAllByNameIgnoreCaseContaining(filter, paged);
	}
	@GetMapping(path="/allFiltered")
	public @ResponseBody
	Iterable<Shop> getAllFiltered(@RequestParam String filter) {
		// This returns a JSON or XML with the users
		if (filter.isEmpty() || filter == null) {
			return repository.findAll();
		}
		return repository.findAllByNameIgnoreCaseContaining(filter);
	}
	@GetMapping(path="/find")
	public @ResponseBody Shop find(@RequestParam Optional<UUID> id, @RequestParam Optional<Boolean> isShopForCreatingHero) {
		// This returns a JSON or XML with the users
		if (isShopForCreatingHero.isPresent()) {
			return repository.findBySystemDefaultAndName(true, ShopTokens.HeroCreationShopName);
		}
		return repository.findById(id.get()).get();
	}

	@PostMapping(path="/create")
	public @ResponseBody JsonObject createRace(@RequestBody Shop raceDto) {
		// This returns a JSON or XML with the users
		repository.save(raceDto);
		
		return Json.createObjectBuilder()
				.add("text", "race saved with success").build();
	}
	@PutMapping(path="/update")
	public @ResponseBody JsonObject updateRace(@RequestBody Shop raceDto) {
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
