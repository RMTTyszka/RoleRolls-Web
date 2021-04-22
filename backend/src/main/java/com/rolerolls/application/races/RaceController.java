package com.rolerolls.application.races;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.rolerolls.application.universes.UniversesQueries;
import com.rolerolls.domain.races.Race;
import com.rolerolls.domain.races.RaceRepository;
import com.rolerolls.domain.universes.UniverseType;
import com.rolerolls.shared.BaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import static org.springframework.data.jpa.domain.Specification.where;


@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/races",  produces = "application/json; charset=UTF-8") // This means URL's start with /demo (after Application path)
public class RaceController extends BaseCrudController<Race,Race, Race, RaceRepository> {

	@Autowired
	private RaceRepository repository;

	public RaceController(RaceRepository repository) {
		super(repository);
		this.repository = repository;
	}

	@Override
	public Race getNew() {
		return new Race();
	}
	@Override
	@GetMapping()
	public @ResponseBody
	Page<Race> getList(@RequestParam String filter, @RequestParam int skipCount, @RequestParam int maxResultCount, @RequestHeader("universe-type") UniverseType universeType) throws JsonProcessingException {
		Pageable paged = PageRequest.of(skipCount, maxResultCount);
		return repository.findAll(where(UniversesQueries.fromUniverse(universeType)), paged);
	}
	@Override
	protected Race createInputToEntity(Race race) {
		return race;
	}

	@Override
	protected Race updateInputToEntity(Race race) {
		return race;
	}

}
