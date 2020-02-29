package com.loh.shared;

import java.util.Arrays;
import java.util.List;

import javax.persistence.Embeddable;

@Embeddable
public class OldSkills {
	public Integer openLocks;
	public Integer steal;
	public Integer operateTrap;
	public Integer jump;
	public Integer climb;
	public Integer sports;
	public Integer diplomacy;
	public Integer bluff;
	public Integer intimidate;
	public Integer arcane;
	public Integer religion;
	public Integer nature;
	public Integer perceive;
	public Integer feeling;
	public Integer search;
	public Integer hide;
	public Integer stealth;
	public Integer acrobacy;
	public Integer wounds;
	public Integer toxicity;
	public Integer curse;
	public Integer attack;
	public Integer evasion;
	public Integer specialAttack;
	public Integer resistance;
	
	public Integer openLocksBonusPoints;
	public Integer stealBonusPoints;
	public Integer operateTrapBonusPoints;
	public Integer jumpBonusPoints;
	public Integer climbBonusPoints;
	public Integer sportsBonusPoints;
	public Integer diplomacyBonusPoints;
	public Integer bluffBonusPoints;
	public Integer intimidateBonusPoints;
	public Integer arcaneBonusPoints;
	public Integer religionBonusPoints;
	public Integer natureBonusPoints;
	public Integer perceiveBonusPoints;
	public Integer feelingBonusPoints;
	public Integer searchBonusPoints;
	public Integer hideBonusPoints;
	public Integer stealthBonusPoints;
	public Integer acrobacyBonusPoints;
	public Integer woundsBonusPoints;
	public Integer toxicityBonusPoints;
	public Integer curseBonusPoints;
	public Integer attackBonusPoints;
	public Integer evasionBonusPoints;
	public Integer specialAttackBonusPoints;
	public Integer resistanceBonusPoints;
	
	public static List<String> getList(){
		return Arrays.asList("openLocks", "steal", "operateTrap", "jump", "climb",
				"sports", "diplomacy", "bluff", "intimidate", "arcane","religion",
				"nature","perceive","feeling","search","hide","stealth","acrobacy",
				"wounds","toxicity","curse", "attack","evasion","specialAttack", "resistance");
	}
}
