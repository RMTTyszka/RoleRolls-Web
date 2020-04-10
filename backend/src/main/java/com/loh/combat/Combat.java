package com.loh.combat;

import com.loh.creatures.Creature;
import com.loh.creatures.heroes.Hero;
import com.loh.creatures.monsters.Monster;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.CollectionTable;
import javax.persistence.ElementCollection;
import javax.persistence.Entity;
import javax.persistence.OneToMany;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

@Entity
public class Combat extends com.loh.shared.Entity {

	@OneToMany
	@Getter
	@Setter
	private List<Monster> monsters = new ArrayList<>();
	@OneToMany
	@Getter
	@Setter
	private List<Hero> heroes = new ArrayList<>();

	@Getter
	@Setter
	private Integer currentInitiative;
	@Getter
	@Setter
	private boolean hasStarted;

	@ElementCollection
	@CollectionTable()
	@Getter
	private List<Initiative> initiatives;


	public Creature getCurrentCreatureTurn() {
		if (currentInitiative != null && this.initiatives != null) return initiatives.stream().filter(e -> !e.isActed()).sorted(Comparator.comparingInt(o -> Math.abs(o.getValue() - currentInitiative))).findFirst().get().getCreature();
		return null;
	}

	public void setInitiatives(List<Initiative> initiatives) {
		Collections.sort(initiatives);
		this.initiatives = initiatives;
	}

	public Initiative addHero(Hero hero, CombatService combatService) {
		this.heroes.add(hero);
		return addInitiative(hero, combatService.rollForInitiative(hero));
	}
	private Initiative addInitiative(Creature creature, Integer initiativeValue) {
		Initiative initiative = new Initiative(creature, initiativeValue);
		this.initiatives.add(initiative);
		return initiative;
	}

	public int closest(int of, int[] in) {
		int min = Integer.MAX_VALUE;
		int closest = of;

		for (int v : in) {
			final int diff = Math.abs(v - of);

			if (diff < min) {
				min = diff;
				closest = v;
			}
		}

		return closest;

	}

	public void addInitiative(Initiative initiative) {
		initiatives.add(initiative);
	}

	public Initiative addMonster(Monster monster, CombatService combatService) {
		this.monsters.add(monster);
		return addInitiative(monster, combatService.rollForInitiative(monster));
	}
}
