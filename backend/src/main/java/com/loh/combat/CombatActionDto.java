package com.loh.combat;

import lombok.Getter;

public class CombatActionDto {
	@Getter
	private AttackDetails attackDetails;
	@Getter
	private Integer evasion;

	public CombatActionDto(AttackDetails attackDetails, Integer mainWeaponBonus, Integer offWeaponBonus, Integer evasion) {
		this.attackDetails = attackDetails;
		this.evasion = evasion;
	}
}
