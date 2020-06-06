package com.loh.shared;

import lombok.Getter;
import lombok.Setter;
import org.hibernate.annotations.GenericGenerator;

import javax.persistence.Column;
import javax.persistence.Embeddable;
import javax.persistence.GeneratedValue;
import java.util.UUID;

@Embeddable
public class Bonus  {
	@GeneratedValue(generator="UUIDgenerator")
	@GenericGenerator(name="UUIDgenerator", strategy="org.hibernate.id.UUIDGenerator")
	@Getter
	@Setter
	@Column(columnDefinition = "BINARY(16)")
	private UUID id;

	@Getter
	@Setter
	private BonusDuration duration;

	@Getter
	@Setter
	private int remainingTurns;

	public Bonus() {
		if (id == null) {
			id = UUID.randomUUID();
		}
	}
	public void update(Bonus bonus){
		property = bonus.property;
		level = bonus.level;
		this.bonus = bonus.bonus;
		duration = bonus.duration;
		remainingTurns = bonus.remainingTurns;
		bonusType = bonus.bonusType;
	}

    public Bonus(String prop, Integer level, Integer bonus, BonusType bonusType) {
		id = UUID.randomUUID();
		this.property = prop;
		this.level = level;
		this.bonus = bonus;
		this.bonusType = bonusType;
	}
	public String getProperty() {
		return property;
	}

	public void setProperty(String name) {
		this.property = name;
	}

	public Integer getBonus() {
		return bonus;
	}

	public void setBonus(Integer value) {
		this.bonus = value;
	}

    
    public Integer getLevel() {
		return level;
	}
	public void setLevel(Integer level) {
		this.level = level;
	}


	public BonusType getBonusType() {
		return bonusType;
	}
	public void setBonusType(BonusType bonusType) {
		this.bonusType = bonusType;
	}


	private String property;
    
    private Integer bonus = 0;
    
    private Integer level = 0;
    
    private BonusType bonusType;

    public int processEndOfTurn() {
    	if (duration == BonusDuration.ByTurn) {
    		this.remainingTurns --;
		}
    	return this.remainingTurns;
	}
}
