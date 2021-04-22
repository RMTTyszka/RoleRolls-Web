package com.rolerolls.domain.roles;

import com.rolerolls.domain.creatures.CreatureType;
import com.rolerolls.domain.universes.UniverseType;
import com.rolerolls.shared.Bonus;
import com.rolerolls.shared.DefaultEntity;
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

	@Getter @Setter
	public UniverseType universeType;
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

    private String name;
    
	@ElementCollection
	@CollectionTable()
    private List<Bonus> bonuses;
    
    public Role(){
    	name = "";
    	bonuses = new ArrayList<>();
	}

	public Role(String name, List<Bonus> bonuses, CreatureType creatureType, UniverseType universeType, boolean isSystemDefault) {
		this.name = name;
		this.bonuses = bonuses;
		this.creatureType = creatureType;
		this.universeType = universeType;
		this.setSystemDefault(isSystemDefault);
	}

	public Integer getAttributePoints(String attr) {
		Optional<Bonus> existingBonus = bonuses.stream().filter(bonus -> bonus.getProperty() == attr).findFirst();
		return existingBonus.isPresent() ? existingBonus.get().getLevel() : 0;
	}

}
