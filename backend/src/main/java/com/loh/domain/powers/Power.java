package com.loh.domain.powers;

import com.loh.shared.DefaultEntity;

import javax.persistence.Entity;

@Entity
public class Power extends DefaultEntity {

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
