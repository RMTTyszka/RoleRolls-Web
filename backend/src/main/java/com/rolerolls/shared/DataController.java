package com.rolerolls.shared;

import com.rolerolls.domain.creatures.Attributes;
import com.rolerolls.domain.skills.Skill;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import java.util.List;


@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/data",  produces = "application/json; charset=UTF-8")
public class DataController {
	@GetMapping(path="/attributes")
	public @ResponseBody List<String> getAllAttributes() {
		
		// This returns a JSON or XML with the users
		return Attributes.getList();
	}
	@GetMapping(path="/skills")
	public @ResponseBody List<String> getAllSkills() {
		// This returns a JSON or XML with the users
		return Skill.getListOld();
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
	@GetMapping(path="/properties")
	public @ResponseBody List<String> getProperties() {
		// This returns a JSON or XML with the users
		return Properties.List;
	}

	
}
