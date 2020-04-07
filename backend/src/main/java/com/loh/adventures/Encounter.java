package com.loh.adventures;

import com.loh.creatures.heroes.Hero;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.OneToMany;
import java.util.ArrayList;
import java.util.List;

@Entity
public class Encounter extends com.loh.shared.Entity {

	private Integer level;

	@OneToMany
	@Getter
	@Setter
	private List<Hero> monsters = new ArrayList<>();

	@OneToMany
	@Getter
	@Setter
	private List<Enviroment> enviroments;

	public List<Hero> getMonsters() {
		return monsters;
	}

	public void setMonsters(List<Hero> monsters) {
		this.monsters = monsters;
	}



	public Integer getLevel() {
		return level;
	}

	public void setLevel(Integer level) {
		this.level = level;
	}

}
