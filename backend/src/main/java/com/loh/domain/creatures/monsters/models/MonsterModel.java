package com.loh.domain.creatures.monsters.models;

import com.loh.application.adventures.environments.Enviroment;
import com.loh.domain.creatures.Attributes;
import com.loh.domain.races.Race;
import com.loh.domain.roles.Role;
import com.loh.shared.Bonus;
import com.loh.shared.DefaultEntity;
import lombok.Getter;
import lombok.Setter;
import org.w3c.dom.Attr;

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
		mainSkills = new ArrayList<>();
	}

	public MonsterModel(Race race, Role role, Attr attributes) {
	}


	private String name;
	@Getter
	@Setter
	private Attributes attributes;
	
	@ElementCollection
	@Getter
	@Setter	private List<String> mainSkills;
	
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
