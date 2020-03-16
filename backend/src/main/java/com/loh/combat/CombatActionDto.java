package com.loh.combat;

import lombok.Getter;

public class CombatActionDto {
	@Getter
	private AttackDetails attackDetail;
	@Getter
	private Integer evasion;

	public CombatActionDto(AttackDetails attackDetail, Integer mainWeaponBonus, Integer offWeaponBonus, Integer evasion) {
		this.attackDetail = attackDetail;
		this.evasion = evasion;
	}
}
