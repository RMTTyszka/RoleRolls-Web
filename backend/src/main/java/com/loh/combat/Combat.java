package com.loh.combat;

import com.loh.creatures.heroes.Hero;
import com.loh.creatures.monsters.Monster;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.CollectionTable;
import javax.persistence.ElementCollection;
import javax.persistence.Entity;
import javax.persistence.OneToMany;
import java.util.ArrayList;
import java.util.List;

@Entity
public class Combat extends com.loh.shared.Entity {

	@OneToMany
	private List<Monster> monsters = new ArrayList<>();
	@OneToMany
	private List<Hero> heroes = new ArrayList<>();

	public List<Monster> getMonsters() {
		return monsters;
	}

	@ElementCollection
	@CollectionTable()
	@Getter
	@Setter
	private List<Iniciative> iniciatives;

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
