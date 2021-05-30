package com.rolerolls.domain.creatures.monsters.models;

import com.rolerolls.application.environments.Enviroment;
import com.rolerolls.domain.creatures.Attributes;
import com.rolerolls.domain.races.Race;
import com.rolerolls.domain.roles.Role;
import com.rolerolls.shared.Bonus;
import com.rolerolls.shared.DefaultEntity;
import com.sun.org.apache.xpath.internal.operations.Equals;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.util.ArrayList;
import java.util.List;

@Entity
public class MonsterModel extends DefaultEntity {

	public MonsterModel() {
		race = new Race();
		role = new Role();
		attributes = new Attributes();
		bonuses = new ArrayList<>();
		skills = new ArrayList<MonsterTemplateSkill>();
	}

	private String name;
	@Getter
	@Setter
	private Attributes attributes;
	

	@Getter
	@Setter
	@ElementCollection
	@CollectionTable()
	private List<MonsterTemplateSkill> skills;
	@Getter
	@Setter
	@ElementCollection
	@CollectionTable()
	private List<String> mainAttributes;


	public List<Enviroment> getEnviroment() {
		return enviroment;
	}
	public void setEnviroment(List<Enviroment> enviroment) {
		this.enviroment = enviroment;
	}
	public List<Bonus> getBonuses() {
		return bonuses;
	}
	public void setBonuses(List<Bonus> bonuses) {
		this.bonuses = bonuses;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public Race getRace() {
		return race;
	}
	public void setRace(Race race) {
		this.race = race;
	}
	public Role getRole() {
		return role;
	}
	public void setRole(Role role) {
		this.role = role;
	}

	@ManyToOne
	private Race race;
	@ManyToOne
	private Role role;
	
	@ElementCollection
	@CollectionTable()
	private List<Enviroment> enviroment = new ArrayList<Enviroment>();
	
	@ElementCollection
	@CollectionTable()
	protected List<Bonus> bonuses = new ArrayList<Bonus>();

}
