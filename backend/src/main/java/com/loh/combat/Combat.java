package com.loh.combat;

import com.loh.creatures.heroes.Hero;
import com.loh.creatures.monsters.Monster;

import javax.persistence.Entity;
import javax.persistence.OneToMany;
import java.util.ArrayList;
import java.util.List;

@Entity
public class Combat extends com.loh.shared.Entity {

	private String name;
	
	@OneToMany
	private List<Monster> monsters = new ArrayList<>();
	@OneToMany
	private List<Hero> heroes = new ArrayList<>();

	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public List<Monster> getMonsters() {
		return monsters;
	}

	public void setMonsters(List<Monster> monsters) {
		this.monsters = monsters;
	}
	public List<Hero> getHeroes() {
		return heroes;
	}
	public void setHeroes(List<Hero> heroes) {
		this.heroes = heroes;
	}
}
