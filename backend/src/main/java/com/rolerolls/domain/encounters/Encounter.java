package com.rolerolls.domain.encounters;

import com.rolerolls.application.environments.Enviroment;
import com.rolerolls.domain.creatures.monsters.models.MonsterModel;
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
	private List<MonsterModel> monsters = new ArrayList<>();

	@OneToMany
	@Getter
	@Setter
	private List<Enviroment> enviroments;
	@Getter
	@Setter
	private String name;

	public Encounter(String name, Integer level, List<MonsterModel> monsters, List<Enviroment> enviroments) {
		this.level = level;
		this.name = name;
		this.monsters = monsters;
		this.enviroments = enviroments;
	}

	public Encounter() {

	}

	public Encounter(String name, Integer level) {
		this.level = level;
		this.name = name;
	}


	public Integer getLevel() {
		return level;
	}

	public void setLevel(Integer level) {
		this.level = level;
	}

	public Boolean addMonsterTemplate(MonsterModel monsterTemplate) {
		boolean alreadyHasMonsterTemplate = this.getMonsters().stream().anyMatch(e -> e.getId().equals(monsterTemplate.getId()));
		if (alreadyHasMonsterTemplate) {
			return false;
		} else {
			this.monsters.add(monsterTemplate);
			return true;
		}
	}
}
