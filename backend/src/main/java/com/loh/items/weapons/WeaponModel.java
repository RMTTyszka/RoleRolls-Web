package com.loh.items.weapons;

import com.loh.items.Equipable;

import javax.persistence.Entity;

@Entity
public class WeaponModel extends Equipable {
	
	private String weaponName;
	
	private Integer numberOfHands;
	
	private String hitAttribute;
	
	private String damageAttribute;
	
	private Integer attributeMod;
	private Integer bonusMod;

	public String getWeaponName() {
		return weaponName;
	}

	public void setWeaponName(String weaponName) {
		this.weaponName = weaponName;
	}

	public Integer getNumberOfHands() {
		return numberOfHands;
	}

	public void setNumberOfHands(Integer numberOfHands) {
		this.numberOfHands = numberOfHands;
	}

	public String getDamageAttribute() {
		return damageAttribute;
	}

	public void setDamageAttribute(String damageAttribute) {
		this.damageAttribute = damageAttribute;
	}

	public String getHitAttribute() {
		return hitAttribute;
	}

	public void setHitAttribute(String hitAttribute) {
		this.hitAttribute = hitAttribute;
	}

	public Integer getAttributeMod() {
		return attributeMod;
	}

	public void setAttributeMod(Integer attributeMod) {
		this.attributeMod = attributeMod;
	}

	public Integer getBonusMod() {
		return bonusMod;
	}

	public void setBonusMod(Integer bonusMod) {
		this.bonusMod = bonusMod;
	}


}
