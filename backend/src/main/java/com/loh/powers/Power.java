package com.loh.powers;

import javax.persistence.Entity;

@Entity
public class Power extends com.loh.shared.Entity {

	private PowerCategory category;
	
	private String description;

	private String name;

	public PowerCategory getCategory() {
		return category;
	}

	public String getDescription() {
		return description;
	}
	public String getName() {
		return name;
	}

	public void setCategory(PowerCategory category) {
		this.category = category;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public void setName(String name) {
		this.name = name;
	}
}
