package com.loh.combat;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.loh.creatures.Creature;
import com.loh.creatures.heroes.Hero;
import com.loh.creatures.monsters.Monster;
import lombok.Getter;
import lombok.Setter;
import org.hibernate.annotations.UpdateTimestamp;

import javax.persistence.*;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.lang.reflect.Type;
import java.util.*;

@Entity
public class Combat extends com.loh.shared.Entity {


	@Getter
	@Setter
	@UpdateTimestamp
	private Date lastUpdateTime;
	@Getter
	@Setter
	private String name;
	@Lob
	@Getter
	private byte[] combatLogSerialized;

	public void addLog(String log) throws UnsupportedEncodingException {
		Gson gson = new Gson();
		Type listType = new TypeToken<ArrayList<CombatLog>>(){}.getType();
		List<CombatLog> logs;
		if (combatLogSerialized != null) {
			String string = new String(combatLogSerialized, "UTF-8");
			logs = gson.fromJson(string, listType);
		} else {
			logs = new ArrayList<>();
		}
		logs.add(new CombatLog(log));
		String json = gson.toJson(logs);
		byte[] bytes = json.getBytes();
		combatLogSerialized = bytes;
	}

	public List<CombatLog> getCombatLog() throws IOException {
		if (combatLogSerialized == null) {
			return new ArrayList<>();
		}
		Gson gson = new Gson();
		String string = new String(combatLogSerialized, "UTF-8");
		Type listType = new TypeToken<ArrayList<CombatLog>>(){}.getType();
		List<CombatLog> logs = gson.fromJson(string, listType);
		return logs;
	}

    @Getter
    @Setter
    @Column(columnDefinition = "BINARY(16)")
    private UUID campaignId;

	@OneToMany(fetch = FetchType.LAZY)
	@Getter
	@Setter
	private List<Monster> monsters = new ArrayList<>();
	@OneToMany(fetch = FetchType.LAZY)
	@Getter
	@Setter
	private List<Hero> heroes = new ArrayList<>();

	@Getter
	@Setter
	private boolean hasStarted;

	@ElementCollection(fetch = FetchType.LAZY)
	@CollectionTable()
	private List<Initiative> initiatives;

	public Initiative getCurrentInitiative() {
		if (this.getInitiatives() != null) {
			Initiative initiative = initiatives.stream().filter(e -> !e.isActed()).sorted().findFirst().orElse(null);
			return initiative;
		}
		return null;
	}

	public void setInitiatives(List<Initiative> initiatives) {
		Collections.sort(initiatives);
		this.initiatives = initiatives;
	}
	public List<Initiative> getInitiatives() {
		Collections.sort(initiatives);
		return initiatives;
	}

	public Initiative addHero(Hero hero, CombatService combatService) {
		this.heroes.add(hero);
		return addInitiative(hero, combatService.rollForInitiative(hero));
	}
	public void removeHero(Hero hero) {
		this.heroes.removeIf(h -> h.getId().equals(hero.getId()));
		removeInitiative(hero);
	}
	public void removeMonster(Monster monster) {
		this.monsters.removeIf(h -> h.getId().equals(monster.getId()));
		removeInitiative(monster);
	}

	private void removeInitiative(Creature creature) {
		initiatives.removeIf(h -> h.getCreature().getId().equals(creature.getId()));
	}

	private Initiative addInitiative(Creature creature, Integer initiativeValue) {
		Initiative initiative = new Initiative(creature, initiativeValue);
		this.initiatives.add(initiative);
		return initiative;
	}
	public void addInitiative(Initiative initiative) {
		initiatives.add(initiative);
	}

	public Initiative addMonster(Monster monster, CombatService combatService) {
		this.monsters.add(monster);
		return addInitiative(monster, combatService.rollForInitiative(monster));
	}

	public void endTurn(Creature creature, CombatRepository combatRepository) {
		creature.processEndOfTurn();
		Initiative initiative = this.initiatives.stream().filter(e -> e.getCreature().getId() == creature.getId()).findFirst().get();
		initiative.setActed(true);
		if (isLastTurn()) {
			processLastTurn();
		}
		combatRepository.save(this);
	}

	private void processLastTurn() {
		for (Initiative initiative : initiatives) {
			initiative.setActed(false);
		}
	}
	private boolean isLastTurn() {
		return initiatives.stream().filter(i -> !i.isActed()).count() == 0;
	}

	public Creature findCreatureById(UUID id) {
		Hero hero = heroes.stream().filter(h -> h.getId().equals(id)).findFirst().orElse(null);
		return hero != null ? hero : monsters.stream().filter(m -> m.getId().equals(id)).findFirst().get();
	}
}
