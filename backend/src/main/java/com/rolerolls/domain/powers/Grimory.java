package com.rolerolls.domain.powers;

import java.util.HashMap;


import java.util.Map;

import org.springframework.context.annotation.Configuration;
@Configuration
public class Grimory {

	Map<String, PowerInstance> powers = new HashMap<String, PowerInstance>();
	Map<String, Trait> traits = new HashMap<String, Trait>();
	
	public Grimory() {
		powers.put("Elves Eye", null);
		powers.put("Dwarf Stubborness", null);
		powers.put("Magic Affinity", null);
		traits.put("Harmless", null);
		traits.put("Faun Blood", null);
		traits.put("Stable", null);
		
		
	}
	
	
}
