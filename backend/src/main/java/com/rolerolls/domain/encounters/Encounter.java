package com.rolerolls.domain.encounters;

import com.rolerolls.application.environments.Enviroment;
import com.rolerolls.domain.creatures.monsters.models.MonsterModel;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.ElementCollection;
import javax.persistence.Entity;
import javax.persistence.OneToMany;
import javax.persistence.Transient;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;
import java.util.concurrent.atomic.AtomicInteger;

@Entity
public class Encounter extends com.rolerolls.shared.Entity {

	private Integer level;

	@Getter
	@Setter
	@ElementCollection
	private List<UUID> monsterTemplateIds = new ArrayList<>();
	@Transient
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

	public Encounter(String name, Integer level, List<UUID> monsterTemplateIds, List<Enviroment> enviroments) {
		this.level = level;
		this.name = name;
		this.monsterTemplateIds = monsterTemplateIds;
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
		this.monsterTemplateIds.add(monsterTemplate.getId());
		return true;
	}

	public void removeMonsterTemplate(UUID monsterTemplateId) {
		AtomicInteger index = new AtomicInteger(0);
		monsterTemplateIds.removeIf(templateId -> templateId.equals(monsterTemplateId) && index.getAndIncrement() < 1);
	}
}
