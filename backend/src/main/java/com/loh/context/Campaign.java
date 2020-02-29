package com.loh.context;

import java.util.ArrayList;
import java.util.List;

import javax.persistence.CollectionTable;
import javax.persistence.ElementCollection;
import javax.persistence.Entity;
import javax.persistence.OneToOne;

import com.loh.adventures.Adventure;

@Entity
public class Campaign extends com.loh.shared.Entity {

	private String name;
	
	@OneToOne
	private Player master;
	
	@ElementCollection
	@CollectionTable()
	private List<Adventure> adventures = new ArrayList<Adventure>();
	
	public Player getMaster() {
		return master;
	}

	public void setMaster(Player master) {
		this.master = master;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}
	
	
}
