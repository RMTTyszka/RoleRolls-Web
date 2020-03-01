package com.loh.items.weapons.weaponModel;

import com.loh.items.Equipable;
import com.loh.items.EquipableSlot;
import com.loh.items.weapons.baseWeapon.BaseWeapon;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.ManyToOne;

@Entity
public class WeaponModel extends Equipable {

	@Getter @Setter
	protected EquipableSlot slot = EquipableSlot.MainHand;
	@Getter @Setter	@ManyToOne
	private BaseWeapon baseWeapon = new BaseWeapon();

	@Getter @Setter
	private boolean isStatic = false;
}
