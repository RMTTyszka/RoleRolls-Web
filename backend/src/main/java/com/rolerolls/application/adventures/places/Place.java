package com.rolerolls.application.adventures.places;

import com.rolerolls.application.adventures.encounters.Encounter;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.util.ArrayList;
import java.util.List;

@Entity
public class Place extends com.rolerolls.shared.Entity {

	private String name;


	@OneToMany
	@Getter
	@Setter
	private List<Encounter> encounters = new ArrayList<Encounter>();

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}
}
