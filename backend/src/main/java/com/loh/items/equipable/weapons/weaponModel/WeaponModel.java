package com.loh.items.equipable.weapons.weaponModel;

import com.loh.items.EquipableSlot;
import com.loh.items.EquipableTemplate;
import com.loh.items.ItemTemplateType;
import com.loh.items.equipable.weapons.baseWeapon.BaseWeapon;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.ManyToOne;

@Entity
public class WeaponModel extends EquipableTemplate {
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
