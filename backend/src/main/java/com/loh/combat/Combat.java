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
import java.util.List;
import java.util.stream.Collectors;

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

	@ElementCollection
	@CollectionTable()
	@Getter
	private List<Initiative> initiatives;


	public Creature currentCreatureTurn() {
		return closest(currentInitiative, initiatives.stream().filter(e -> !e.isActed()).mapToInt(e -> e.getValue()).toArray());
	}

	public void setInitiatives(List<Initiative> initiatives) {
		Collections.sort(initiatives);
		this.initiatives = initiatives;
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
