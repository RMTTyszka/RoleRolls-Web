package com.loh.creatures.monsters;

import java.util.ArrayList;
import java.util.List;

import javax.persistence.CollectionTable;
import javax.persistence.ElementCollection;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.ManyToOne;

import com.loh.adventures.Enviroment;
import com.loh.race.Race;
import com.loh.role.Role;
import com.loh.shared.OldAttributes;
import com.loh.shared.Bonus;

@Entity
public class MonsterBase {
	
	private String name;
	private OldAttributes oldAttributes;
	
	@ElementCollection
	private List<String> mainSkills;
	
	public List<String> getMainSkills() {
		return mainSkills;
	}
	public void setMainSkills(List<String> mainSkills) {
		this.mainSkills = mainSkills;
	}
	public Integer getId() {
		return id;
	}
	public void setId(Integer id) {
		this.id = id;
	}
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
	
	public OldAttributes getOldAttributes() {
		return oldAttributes;
	}
	public void setOldAttributes(OldAttributes oldAttributes) {
		this.oldAttributes = oldAttributes;
	}

	@Id
    @GeneratedValue(strategy=GenerationType.AUTO)
    private Integer id;
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
