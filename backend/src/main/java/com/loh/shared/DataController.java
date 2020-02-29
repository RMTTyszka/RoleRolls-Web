package com.loh.shared;

import java.util.List;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;


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
		return OldAttributes.getList();
	}
	@GetMapping(path="/skills")
	public @ResponseBody List<String> getAllSkills() {
		// This returns a JSON or XML with the users
		return OldSkills.getList();
	}
	@GetMapping(path="/configs")
	public @ResponseBody List<String> getConfigs() {
		// This returns a JSON or XML with the users
		return OldSkills.getList();
	}
	@GetMapping(path="/levelDetails")
	public @ResponseBody LevelDetails getNextLevel(@RequestParam Integer level) {
		// This returns a JSON or XML with the users
		return new LevelDetails(level);
	}

	
}
