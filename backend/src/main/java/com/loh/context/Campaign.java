package com.loh.context;

import com.loh.adventures.Adventure;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

@Entity
public class Campaign extends com.loh.shared.Entity {

	@Getter
	@Setter
	private String name;
	
	@OneToOne
	private Player master;

	@ManyToMany
	@Getter
	@Setter
	private Set<Player> players = new HashSet<>();
	
	@OneToMany
	@Getter
	@Setter
	private List<Adventure> adventures = new ArrayList<Adventure>();
	

}
