package com.loh.shared;

import javax.persistence.Embeddable;

@Embeddable
public class Bonus  {


	public Bonus() {}

    public Bonus(String prop, Integer level, Integer bonus, BonusType bonusType) {
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
}
