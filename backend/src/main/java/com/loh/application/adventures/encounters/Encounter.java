package com.loh.application.adventures.encounters;

import com.loh.application.adventures.environments.Enviroment;
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

	public Integer getLevel() {
		return level;
	}

	public void setLevel(Integer level) {
		this.level = level;
	}

}
