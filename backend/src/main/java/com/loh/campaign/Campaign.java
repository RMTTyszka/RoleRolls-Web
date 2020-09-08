package com.loh.campaign;

import com.loh.adventures.Adventure;
import com.loh.campaign.dtos.HeroNotFromAddedPlayerException;
import com.loh.context.Player;
import com.loh.creatures.heroes.Hero;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.ManyToMany;
import javax.persistence.OneToMany;
import javax.persistence.OneToOne;
import java.util.*;

@Entity
public class Campaign extends com.loh.shared.Entity {

	@Getter
	@Setter
	private String name;
	@Getter
	@Setter
	private String description;
	
	@OneToOne
	@Getter
	@Setter
	private Player master;

	@ManyToMany
	@Getter
	@Setter
	private Set<Player> players = new HashSet<>();

	@OneToMany
	@Getter
	@Setter
	private Set<Hero> heroes = new HashSet<>();
	
	@OneToMany
	@Getter
	@Setter
	private List<Adventure> adventures = new ArrayList<Adventure>();
	

	public void addPlayer(Player player) {
		players.add(player);
	}
	public void removePlayer(UUID playerId) {
		players.removeIf(p -> p.getId().equals(playerId));
	}

	public void addHero(Hero hero) throws HeroNotFromAddedPlayerException {
		boolean isHeroFromAddedPLayer = players.stream().anyMatch(p -> p.getId().equals(hero.getOwnerId()));
		if (isHeroFromAddedPLayer) {
			heroes.add(hero);
		} else {
			throw new HeroNotFromAddedPlayerException();
		}
	}
}
