package com.rolerolls.application.adventures.encounters;

import com.rolerolls.application.adventures.environments.Enviroment;
import com.rolerolls.domain.creatures.heroes.Hero;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.OneToMany;
import java.util.ArrayList;
import java.util.List;

@Entity
public class Encounter extends com.rolerolls.shared.Entity {

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
