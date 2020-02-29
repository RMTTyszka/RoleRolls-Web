package com.loh.adventures;

import javax.persistence.*;
import java.util.ArrayList;
import java.util.List;

@Entity
public class Place extends com.loh.shared.Entity {

	private String name;
	
	@ElementCollection
	@CollectionTable()
	private List<Encounter> encounters = new ArrayList<Encounter>();

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}
}
