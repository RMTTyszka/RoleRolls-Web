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

	public Integer getLevel() {
		return level;
	}

	public void setLevel(Integer level) {
		this.level = level;
	}

}
