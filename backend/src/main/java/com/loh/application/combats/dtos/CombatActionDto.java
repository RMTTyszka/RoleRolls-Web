package com.loh.application.combats.dtos;

import com.loh.domain.combats.AttackDetails;
import com.loh.domain.combats.Combat;
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
