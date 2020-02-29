package com.loh.adventures;

import com.loh.creatures.heroes.Hero;

import javax.persistence.CollectionTable;
import javax.persistence.ElementCollection;
import javax.persistence.Entity;
import java.util.ArrayList;
import java.util.List;

@Entity
public class Encounter extends com.loh.shared.Entity {

	private Integer level;
	
	private String name;
	
	@ElementCollection
	@CollectionTable()
	private List<Hero> monsters = new ArrayList<>();

	@ElementCollection
	@CollectionTable()
	private List<Enviroment> enviroments;

	public List<Hero> getMonsters() {
		return monsters;
	}

	public void setMonsters(List<Hero> monsters) {
		this.monsters = monsters;
	}

	public List<Enviroment> getEnviroments() {
		return enviroments;
	}

	public void setEnviroments(List<Enviroment> enviroments) {
		this.enviroments = enviroments;
	}

	public Integer getLevel() {
		return level;
	}

	public void setLevel(Integer level) {
		this.level = level;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}
}
