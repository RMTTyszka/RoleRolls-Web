package com.loh.context;

import com.loh.creatures.heroes.Hero;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.ManyToMany;
import javax.persistence.OneToMany;
import java.util.HashSet;
import java.util.Set;

@Entity
public class Player extends com.loh.shared.Entity {

	@Getter
	@Setter
	private String name;

	@ManyToMany
	@Getter
	@Setter
	private Set<Campaign> campaigns = new HashSet<>();

	@OneToMany
	@Getter
	@Setter
	private Set<Hero> heroes = new HashSet<>();
	
	public Player() {
		
	}
	public Player(String name) {
		this.name = name;
	}

}
