package com.loh.shared;

import com.loh.creatures.heroes.equipment.Equipment;
import com.loh.items.weapons.WeaponModel;
import com.loh.powers.PowerInstance;
import com.loh.race.Race;
import com.loh.role.Role;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import javax.validation.constraints.NotNull;
import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.function.Predicate;
import java.util.stream.Collectors;

@MappedSuperclass
public class OldCreature extends Entity {

	static Integer lifeBase = 4;

	public static Integer getLifeBase() {
		return lifeBase;
	}

	public static void setLifeBase(Integer lifeBase) {
		OldCreature.lifeBase = lifeBase;
	}

	// get 1/2 level of bonus and +2 every 5 levels
	protected OldAttributes oldAttributes;

	@ElementCollection
	@CollectionTable()
	protected List<Bonus> bonuses = new ArrayList<Bonus>();

	@OneToOne
	protected Equipment equipment;

	protected Integer level;
	
	protected Integer experience;
	
	@Transient
	protected Integer nextLevel;

	public Integer getNextLevel() {
		return nextLevel;
	}

	public void setNextLevel(Integer nextLevel) {
		this.nextLevel = nextLevel;
	}

	public Integer getExperience() {
		return experience;
	}

	public void setExperience(Integer experiencie) {
		this.experience = experiencie;
	}

	@Getter @Setter
	protected Integer life;
	@Transient
	protected Integer maxAttributesBonusPoints;

	@Transient
	protected Integer maxSkillsBonusPoints;
	protected Integer moral;

	@NotNull
	protected String name;
	
	@ElementCollection
	@CollectionTable()
	protected List<PowerInstance> powers = new ArrayList<PowerInstance>();
	
	@ManyToOne
	protected Race race;
	@ManyToOne
	protected Role role;

	protected OldSkills oldSkills;

	@Transient
	protected Integer totalAttributesBonusPoints;

	@Transient
	protected Integer totalSkillsBonusPoints;

	@Transient
	public Integer attackBonus(OldCreature attacker, WeaponModel weapon) throws NoSuchFieldException, SecurityException,
			IllegalAccessException, NoSuchMethodException, InvocationTargetException {
		Integer attr = attacker.getAttrMod(weapon.getHitAttribute());
		Integer weap = weapon.getBonus()| 0;
		attr *= weapon.getAttributeMod();
		weap *= weapon.getBonusMod();
		return attr + weap;
	}

	public void calculateBaseAttributes() {
		OldAttributes.getList().forEach(attr -> {
			try {
				calculateBaseAttributeValue(attr);
			} catch (NoSuchFieldException | SecurityException | IllegalArgumentException | IllegalAccessException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		});
	}

	protected void calculateBaseAttributeValue(String attribute) throws NoSuchFieldException, SecurityException, IllegalArgumentException, IllegalAccessException {
		
		Class<?> attributesClass =  OldAttributes.class;

		Field attrField = attributesClass.getField(attribute);
		attrField.setAccessible(true);
		
		Integer value = race.getBonuses().stream().filter(bonus -> bonus.getProperty().equalsIgnoreCase(attribute)).map(bonus -> bonus.getLevel()).reduce((a,  b) -> a + b).orElse(0);
		
		value += role.getBonuses().stream().filter(bonus -> bonus.getProperty().equalsIgnoreCase(attribute)).map(bonus -> bonus.getLevel()).reduce((a,  b) -> a + b).orElse(0);
		
		Field attrFieldBonus = attributesClass.getField(attribute.concat("BonusPoints"));
		attrFieldBonus.setAccessible(true);
		
		
		value += (int)Optional.ofNullable(attrFieldBonus.get(oldAttributes)).orElse(0);

		
		attrField.set(oldAttributes, value);
		
	}

	public void calculateBaseSkills() {
		OldSkills.getList().forEach(sk -> {
			try {
				calculateBaseSkillValue(sk);
			} catch (NoSuchFieldException | SecurityException | IllegalArgumentException | IllegalAccessException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		});
	}

	protected void calculateBaseSkillValue(String sk) throws NoSuchFieldException, SecurityException, IllegalArgumentException, IllegalAccessException {
		
		Class<?> skillsClass =  OldSkills.class;
		
		Field skillField = skillsClass.getField(sk);
		skillField.setAccessible(true);
		
		Integer value = race.getBonuses().stream().filter(bonus -> bonus.getProperty().equalsIgnoreCase(sk)).map(bonus -> bonus.getBonus()).reduce((a,  b) -> a + b).orElse(0);
		
		value += role.getBonuses().stream().filter(bonus -> bonus.getProperty().equalsIgnoreCase(sk)).map(bonus -> bonus.getBonus()).reduce((a,  b) -> a + b).orElse(0);
		
		Field skillFieldBonus = skillsClass.getField(sk+"BonusPoints");
		skillFieldBonus.setAccessible(true);
		
		Object skill = skillFieldBonus.get(oldSkills);
		
		value += skill != null ? (int)skill : 0;
	//	value += Optional.ofNullable(skillFieldBonus.getInt(oldSkills)).orElse(0);
		
		skillField.set(oldSkills, value);
		
	}

	@Transient
	public Integer evasion() throws NoSuchFieldException, SecurityException, IllegalAccessException,
			NoSuchMethodException, InvocationTargetException {
		Integer agi = this.getAttrMod("agility");
		Integer base = 10;
		Integer armor = this.getEquipment().getArmor().getArmorModel().getBonus() | 0;
		return base + agi + armor;
	}

	public Integer getAttrBonusDice(String attr) throws NoSuchFieldException, SecurityException, IllegalAccessException,
			NoSuchMethodException, InvocationTargetException {
		return (this.getAttrLevel(attr) + 4) % 5 * 4;

	}

	public OldAttributes getOldAttributes() {
		return oldAttributes;
	}

	public Integer getAttrLevel(String attr) throws NoSuchFieldException, SecurityException, IllegalAccessException,
			InvocationTargetException, NoSuchMethodException {
		String attribute = attr.toLowerCase();

		level = this.getTotalBonusLevel(attr);
		Field f = this.oldAttributes.getClass().getDeclaredField(attribute);
		f.setAccessible(true);
		System.out.println(f.get(this.oldAttributes));
		try {
			return ((Integer) f.get(this.oldAttributes)) + level;

		} catch (IllegalArgumentException | SecurityException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return 0;
	}

	public Integer getAttrMod(String attr) throws NoSuchFieldException, SecurityException, IllegalAccessException,
			NoSuchMethodException, InvocationTargetException {
		return (this.getAttrLevel(attr) + 4) / 5;

	}

	public List<Bonus> getBonuses() {
		return bonuses;
	}

	public Equipment getEquipment() {
		return equipment;
	}

	@Transient
	public Integer getEquipmentBonusLevel(String prop) {
		Predicate<Bonus> byProperty = b -> b.getProperty().toLowerCase() == prop.toLowerCase()
				&& b.getBonusType() == BonusType.Magical;
		return this.bonuses.stream().filter(byProperty).collect(Collectors.toList()).size() > 0 ? this.bonuses.stream()
				.filter(byProperty).map(bon -> bon.getLevel()).mapToInt(Integer::intValue).max().getAsInt() : 0;
	}

	@Transient
	public Integer getEquipmentBonusValue(String prop) {
		Predicate<Bonus> byProperty = b -> b.getProperty().toLowerCase() == prop.toLowerCase()
				&& b.getBonusType() == BonusType.Magical;
		return this.bonuses.stream().filter(byProperty).collect(Collectors.toList()).size() > 0 ? this.bonuses.stream()
				.filter(byProperty).map(bon -> bon.getBonus()).mapToInt(Integer::intValue).max().getAsInt() : 0;
	}

	@Transient
	public Integer getInnateBonusLevel(String prop) {
		Predicate<Bonus> byProperty = b -> b.getProperty().toLowerCase() == prop.toLowerCase()
				&& b.getBonusType() == BonusType.Innate;
		return this.bonuses.stream().filter(byProperty).collect(Collectors.toList()).size() > 0
				? this.bonuses.stream().filter(byProperty).map(bon -> bon.getLevel()).mapToInt(Integer::intValue).sum()
				: 0;
	}

	@Transient
	public Integer getInnateBonusValue(String prop) {
		Predicate<Bonus> byProperty = b -> b.getProperty().toLowerCase() == prop.toLowerCase()
				&& b.getBonusType() == BonusType.Innate;
		return this.bonuses.stream().filter(byProperty).collect(Collectors.toList()).size() > 0
				? this.bonuses.stream().filter(byProperty).map(bon -> bon.getBonus()).mapToInt(Integer::intValue).sum()
				: 0;
	}

	public Integer getLevel() {
		return level;
	}

	public Integer getLife() {
		return life;
	}

	@Transient
	public Integer getMagicalBonusLevel(String prop) {
		Predicate<Bonus> byProperty = b -> b.getProperty().toLowerCase() == prop.toLowerCase()
				&& b.getBonusType() == BonusType.Magical;

		return this.bonuses.stream().filter(byProperty).collect(Collectors.toList()).size() > 0 ? this.bonuses.stream()
				.filter(byProperty).map(bon -> bon.getLevel()).mapToInt(Integer::intValue).max().getAsInt() : 0;
	}

	@Transient
	public Integer getMagicalBonusValue(String prop) {
		Predicate<Bonus> byProperty = b -> b.getProperty().toLowerCase() == prop.toLowerCase()
				&& b.getBonusType() == BonusType.Magical;

		return this.bonuses.stream().filter(byProperty).collect(Collectors.toList()).size() > 0 ? this.bonuses.stream()
				.filter(byProperty).map(bon -> bon.getBonus()).mapToInt(Integer::intValue).max().getAsInt() : 0;
	}

	public Integer getMaxAttributesBonusPoints() {
		return level - 1;
	}

	public Integer getMaxLife() throws NoSuchFieldException, SecurityException, IllegalAccessException,
			NoSuchMethodException, InvocationTargetException {
		return 5 + 4 * this.level + (this.level + 2) * this.getAttrMod("vitality");
	}

	public Integer getMaxSkillsBonusPoints() {
		return level + 2;
	}

	public Integer getMoral() {
		return moral;
	}

	public String getName() {
		return name;
	}

	public List<PowerInstance> getPowers() {
		return powers;
	}

	public Race getRace() {
		return race;
	}

	public Role getRole() {
		return role;
	}

	public OldSkills getOldSkills() {
		return oldSkills;
	}

	public Integer getTotalAttributesBonusPoints() {
		return (level -1) * 2;
	}

	public Integer getTotalBonusLevel(String property) {
		return this.getMagicalBonusLevel(property) + this.getEquipmentBonusLevel(property)
				+ this.getInnateBonusLevel(property);
	}

	public Integer getTotalBonusValue(String property) {
		return this.getMagicalBonusValue(property) + this.getEquipmentBonusValue(property)
		+ this.getInnateBonusValue(property);
	}

	public Integer getTotalSkillsBonusPoints() {
		return (level - 1) * 6 + 18;
	}

	public void setOldAttributes(OldAttributes oldAttributes) {
		this.oldAttributes = oldAttributes;
	}

	public void setBonuses(List<Bonus> bonuses) {
		this.bonuses = bonuses;
	}

	public void setEquipment(Equipment equipment) {
		this.equipment = equipment;
	}

	public void setLevel(Integer level) {
		this.level = level;
	}
	// public List<Bonus> getBonuses() {
	// return bonuses;
	// }
	// public void setBonuses(List<Bonus> bonuses) {
	// this.bonuses = bonuses;
	// }
	
	public void setLife(Integer life) {
		this.life = life;
	}

	public void setMaxAttributesBonusPoints(Integer maxAttributesBonusPoints) {
		this.maxAttributesBonusPoints = maxAttributesBonusPoints;
	}
	
	public void setMaxSkillsBonusPoints(Integer maxSkillsBonusPoints) {
		this.maxSkillsBonusPoints = maxSkillsBonusPoints;
	}

	public void setMoral(Integer moral) {
		this.moral = moral;
	}

	public void setName(String name) {
		this.name = name;
	}

	public void setPowers(List<PowerInstance> powers) {
		this.powers = powers;
	}

	public void setRace(Race race) {
		this.race = race;
	}

	public void setRole(Role role) {
		this.role = role;
	}

	public void setOldSkills(OldSkills oldSkills) {
		this.oldSkills = oldSkills;
	}

	public void setTotalAttributesBonusPoints(Integer totalAttributeBonusPoints) {
		this.totalAttributesBonusPoints = totalAttributeBonusPoints;
	}

	public void setTotalSkillsBonusPoints(Integer totalSkillsBonusPoints) {
		this.totalSkillsBonusPoints = totalSkillsBonusPoints;
	}

}
