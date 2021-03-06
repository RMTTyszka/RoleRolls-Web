package com.loh.domain.adventures;

import com.loh.application.adventures.places.Place;
import com.loh.application.adventures.places.PlaceRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/places",  produces = "application/json; charset=UTF-8")
public class PlaceController {

	@Autowired
	private PlaceRepository placeRepository;
	
	@GetMapping(path="/all")
	public @ResponseBody Iterable<Place> getAllPlaces() {
		
		// This returns a JSON or XML with the users
		return placeRepository.findAll();
	}
}
