package com.loh.race;

import com.loh.creatures.CreatureType;
import com.loh.powers.Power;
import com.loh.shared.Bonus;
import com.loh.shared.DefaultEntity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.CollectionTable;
import javax.persistence.ElementCollection;
import javax.persistence.Entity;
import javax.persistence.ManyToMany;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Entity
public class Race extends DefaultEntity {
	
	private String name;

	@Getter	@Setter
	private CreatureType creatureType;

	@ElementCollection
	@CollectionTable()
	private List<Bonus> bonuses;
	
	
	@ManyToMany
	private List<Power> powers;
	
	public List<Power> getPowers() {
		return powers;
	}

	public void setPowers(List<Power> powers) {
		this.powers = powers;
	}

	public List<String> getTraits() {
		return traits;
	}

	public void setTraits(List<String> traits) {
		this.traits = traits;
	}

	@ElementCollection
	@CollectionTable()
	private List<String> traits;

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}


	public List<Bonus> getBonuses() {
		return bonuses;
	}

	public void setBonuses(List<Bonus> bonuses) {
		this.bonuses = bonuses;
	}

	public Race(){
		name = "";
		bonuses = new ArrayList<Bonus>();
		powers = new ArrayList<Power>();
		traits = new ArrayList<String>();
	}

	public Race(String name, List<Bonus> bonuses, List<Power> powers, List<String> traits, CreatureType creatureType) {
		this.name = name;
		this.bonuses = bonuses;
		this.powers = powers;
		this.traits = traits;
		this.creatureType = creatureType;
		this.setSystemDefault(true);
	}

	public Integer getAttributePoints(String attr) {
		Optional<Bonus> existingBonus = bonuses.stream().filter(bonus -> bonus.getProperty() == attr).findFirst();
		return existingBonus.isPresent() ? existingBonus.get().getLevel() : 0;
	}
}
