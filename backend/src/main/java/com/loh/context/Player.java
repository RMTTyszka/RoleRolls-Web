package com.loh.context;

import javax.persistence.*;
import java.util.List;

@Entity
public class Player extends com.loh.shared.Entity {

	private String name;
	
	@ElementCollection
	@CollectionTable()
	private List<Campaign> campaign;
	
	public Player() {
		
	}
	public Player(String name) {
		this.name = name;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}


}
