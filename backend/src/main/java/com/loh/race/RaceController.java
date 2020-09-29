package com.loh.race;

import com.loh.shared.BaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;


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
	public Race getnew() {
		return null;
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
