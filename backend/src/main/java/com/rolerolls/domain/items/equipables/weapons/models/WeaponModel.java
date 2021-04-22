package com.rolerolls.domain.items.equipables.weapons.models;

import com.rolerolls.domain.items.equipables.EquipableSlot;
import com.rolerolls.domain.items.equipables.EquipableTemplate;
import com.rolerolls.domain.items.templates.ItemTemplateType;
import com.rolerolls.domain.items.equipables.weapons.base.BaseWeapon;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.ManyToOne;

@Entity
public class WeaponModel extends EquipableTemplate {
	@Getter @Setter
	protected ItemTemplateType itemTemplateType = ItemTemplateType.Weapon;
	public WeaponModel() {
		baseWeapon  = new BaseWeapon();
	}

	public WeaponModel(String name, BaseWeapon baseWeapon) {
		this.baseWeapon = baseWeapon;
		this.name = name;
	}

	@Getter @Setter
	protected EquipableSlot slot = EquipableSlot.Weapon;
	@Getter @Setter	@ManyToOne
	private BaseWeapon baseWeapon;
}
