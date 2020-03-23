package com.loh.items.equipable.armors.armorModel;

import com.loh.items.Equipable;
import com.loh.items.EquipableSlot;
import com.loh.items.equipable.armors.baseArmor.BaseArmor;
import com.loh.shared.Bonuses;
import com.loh.shared.Properties;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.ManyToOne;

@Entity
public class ArmorModel extends Equipable {

	public ArmorModel(){
	}

	public ArmorModel(String name, BaseArmor baseArmor) {
		this.baseArmor = baseArmor;
		this.name = name;
	}

	@Getter @Setter
	protected EquipableSlot slot = EquipableSlot.Chest;

	@ManyToOne
	@Getter @Setter
	private BaseArmor baseArmor = new BaseArmor();

	public java.lang.Integer getDefense(Integer bonusModifier) {
		return baseArmor.getCategory().getDefense() * bonusModifier + baseArmor.getCategory().getBaseDefense() + Bonuses.GetBonus(bonuses, Properties.Defense);
	}
	public java.lang.Integer getEvasion() {
		return Bonuses.GetBonus(bonuses, Properties.Evasion);
	}
	public java.lang.Integer getDodge() {
		return baseArmor.getCategory().getDodge() + Bonuses.GetBonus(bonuses, Properties.Dodge);
	}

}
