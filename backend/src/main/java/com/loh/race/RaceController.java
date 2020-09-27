package com.loh.race;

import com.loh.shared.BaseCrudController;
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
public class RaceController extends BaseCrudController<Race, RaceRepository> {

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
}
