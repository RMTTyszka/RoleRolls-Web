package com.loh.shared;

import com.loh.creatures.Attributes;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.List;


@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/data",  produces = "application/json; charset=UTF-8")
public class DataController {
	@GetMapping(path="/attributes")
	public @ResponseBody List<String> getAllAttributes() {
		
		for (int i = 0; i < 10; i++) {
			LevelDetails x = new LevelDetails(i + 1);
			System.out.println(x.expToNextLevel);
			
		}
		// This returns a JSON or XML with the users
		return Attributes.getList();
	}
	@GetMapping(path="/skills")
	public @ResponseBody List<String> getAllSkills() {
		// This returns a JSON or XML with the users
		return Attributes.getList();
	}
	@GetMapping(path="/configs")
	public @ResponseBody List<String> getConfigs() {
		// This returns a JSON or XML with the users
		return Attributes.getList();
	}
	@GetMapping(path="/levelDetails")
	public @ResponseBody LevelDetails getNextLevel(@RequestParam Integer level) {
		// This returns a JSON or XML with the users
		return new LevelDetails(level);
	}

	
}
