package com.rolerolls.domain.campaigns;

import com.rolerolls.application.adventures.adventures.Adventure;
import com.rolerolls.authentication.LohUserDetails;
import com.rolerolls.application.campaigns.dtos.HeroNotFromAddedPlayerException;
import com.rolerolls.domain.contexts.Player;
import com.rolerolls.domain.creatures.heroes.Hero;
import lombok.Getter;
import lombok.Setter;
import org.springframework.security.core.context.SecurityContextHolder;

import javax.persistence.Entity;
import javax.persistence.ManyToMany;
import javax.persistence.OneToMany;
import java.util.*;

@Entity
public class Campaign extends com.rolerolls.shared.Entity {

	@Getter
	@Setter
	private String name;
	@Getter
	@Setter
	private String description;
	
	@Getter
	@Setter
	private UUID masterId;

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
	public void removeHero(UUID heroId) {
		heroes.removeIf(p -> p.getId().equals(heroId));
	}
	public void removeHeroFromPlayer(UUID playerId) {
		heroes.removeIf(p -> p.getOwnerId().equals(playerId));
	}
	public boolean isMaster() {
		return ((LohUserDetails) SecurityContextHolder.getContext().getAuthentication().getPrincipal()).getUserId().equals(masterId);
	}
}