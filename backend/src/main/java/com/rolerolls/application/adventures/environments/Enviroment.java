package com.rolerolls.application.adventures.environments;

import javax.persistence.Entity;

@Entity
public class Enviroment extends com.rolerolls.shared.Entity {

	private String name;

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}
	
}
