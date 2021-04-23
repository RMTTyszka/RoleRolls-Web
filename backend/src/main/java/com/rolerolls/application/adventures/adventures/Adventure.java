package com.rolerolls.application.adventures.adventures;

import com.rolerolls.application.adventures.places.Place;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.OneToMany;
import java.util.HashSet;
import java.util.Set;

@Entity
public class Adventure extends com.rolerolls.shared.Entity {

	private String name;
	

	@OneToMany
	@Getter
	@Setter
	private Set<Place> places = new HashSet<>();

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}
	
}
