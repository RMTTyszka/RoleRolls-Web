package com.loh.items.armors.armorModel;

import com.loh.items.Equipable;
import com.loh.items.EquipableSlot;
import com.loh.items.armors.baseArmor.BaseArmor;
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
	@Getter @Setter
	protected EquipableSlot slot = EquipableSlot.Chest;

	@ManyToOne
	@Getter @Setter
	private BaseArmor baseArmor = new BaseArmor();

	@Getter @Setter
	private boolean isStatic = false;

	public java.lang.Integer getDefense() {
		return baseArmor.getCategory().getDefense() * bonus + baseArmor.getCategory().getBaseDefense() + Bonuses.GetBonus(bonuses, Properties.Defense);
	}
	public java.lang.Integer getEvasion() {
		return baseArmor.getCategory().getEvasion() + bonus + Bonuses.GetBonus(bonuses, Properties.Defense);
	}

}
