package com.loh.domain.roles;

import com.loh.domain.creatures.CreatureType;
import com.loh.shared.Bonus;
import com.loh.shared.DefaultEntity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.CollectionTable;
import javax.persistence.ElementCollection;
import javax.persistence.Entity;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;


@Entity
public class Role extends DefaultEntity {
	

	@Getter
	@Setter
	private CreatureType creatureType;
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


	public Integer getSkillPoints() {
		return skillPoints;
	}

	public void setSkillPoints(Integer skillPoints) {
		this.skillPoints = skillPoints;
	}

    private String name;
    
	@ElementCollection
	@CollectionTable()
    private List<Bonus> bonuses;
    
    private Integer skillPoints;

    public Role(){
    	name = "";
    	bonuses = new ArrayList<>();
    	skillPoints = 0;
	}

	public Role(String name, List<Bonus> bonuses, Integer skillPoints, CreatureType creatureType) {
		this.name = name;
		this.bonuses = bonuses;
		this.skillPoints = skillPoints;
		this.creatureType = creatureType;
		this.setSystemDefault(true);
	}

	public Integer getAttributePoints(String attr) {
		Optional<Bonus> existingBonus = bonuses.stream().filter(bonus -> bonus.getProperty() == attr).findFirst();
		return existingBonus.isPresent() ? existingBonus.get().getLevel() : 0;
	}
}
