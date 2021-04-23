package com.rolerolls.application.combats.dtos;

import com.rolerolls.domain.combats.AttackDetails;
import com.rolerolls.domain.combats.Combat;
import lombok.Getter;
import lombok.Setter;

public class CombatActionDto {
	@Getter
	private AttackDetails attackDetails;
	@Getter
	private Integer evasion;
	@Getter
	@Setter
    Combat combat;

	public CombatActionDto(Combat combat, AttackDetails attackDetails, Integer mainWeaponBonus, Integer offWeaponBonus, Integer evasion) {
		this.combat = combat;
		this.attackDetails = attackDetails;
		this.evasion = evasion;
	}
}
