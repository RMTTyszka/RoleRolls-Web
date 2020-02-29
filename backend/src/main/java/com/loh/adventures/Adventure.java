package com.loh.adventures;

import javax.persistence.*;
import java.util.ArrayList;
import java.util.List;

@Entity
public class Adventure extends com.loh.shared.Entity {

	private String name;
	
	@ElementCollection
	@CollectionTable()
	private List<Place> places = new ArrayList<Place>();

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}
	
}
