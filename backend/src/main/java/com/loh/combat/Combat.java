package com.loh.combat;

import com.loh.creatures.heroes.Hero;

import javax.persistence.Entity;
import javax.persistence.OneToMany;
import java.util.ArrayList;
import java.util.List;

@Entity
public class Combat extends com.loh.shared.Entity {

	private String name;
	
	@OneToMany
	private List<Hero> monsters = new ArrayList<Hero>();
	@OneToMany
	private List<Hero> heroes = new ArrayList<Hero>();

	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public List<Hero> getMonsters() {
		return monsters;
	}

	public void setMonsters(List<Hero> monsters) {
		this.monsters = monsters;
	}
	public List<Hero> getHeroes() {
		return heroes;
	}
	public void setHeroes(List<Hero> heroes) {
		this.heroes = heroes;
	}
}
