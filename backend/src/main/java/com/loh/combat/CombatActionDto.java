package com.loh.combat;

import java.util.ArrayList;
import java.util.List;

public class CombatActionDto {
	private List<Integer> mainWeaponHits = new ArrayList<>();
	private List<Integer> offWeaponHits = new ArrayList<>();
	private List<Integer> mainWeaponRolls = new ArrayList<>();
	private List<Integer> offWeaponRolls = new ArrayList<>();
	private Integer mainWeaponBonus;
	private Integer offWeaponBonus;
	private Integer evasion;
	public List<Integer> getMainWeaponRolls() {
		return mainWeaponRolls;
	}
	public void setMainWeaponRolls(List<Integer> mainWeaponRolls) {
		this.mainWeaponRolls = mainWeaponRolls;
	}
	public List<Integer> getOffWeaponRolls() {
		return offWeaponRolls;
	}
	public void setOffWeaponRolls(List<Integer> offWeaponRolls) {
		this.offWeaponRolls = offWeaponRolls;
	}
	public Integer getMainWeaponBonus() {
		return mainWeaponBonus;
	}
	public void setMainWeaponBonus(Integer mainWeaponBonus) {
		this.mainWeaponBonus = mainWeaponBonus;
	}
	public Integer getOffWeaponBonus() {
		return offWeaponBonus;
	}
	public void setOffWeaponBonus(Integer offWeaponBonus) {
		this.offWeaponBonus = offWeaponBonus;
	}
	public Integer getEvasion() {
		return evasion;
	}
	public void setEvasion(Integer evasion) {
		this.evasion = evasion;
	}
	public List<Integer> getMainWeaponHits() {
		return mainWeaponHits;
	}
	public void setMainWeaponHits(List<Integer> mainWeaponHits) {
		this.mainWeaponHits = mainWeaponHits;
	}
	public List<Integer> getOffWeaponHits() {
		return offWeaponHits;
	}
	public void setOffWeaponHits(List<Integer> offWeaponHits) {
		this.offWeaponHits = offWeaponHits;
	}
}
