package com.loh.domain.items.equipables.armors.models;

import com.loh.domain.items.equipables.EquipableTemplate;
import com.loh.domain.items.equipables.EquipableSlot;
import com.loh.domain.items.ItemTemplateType;
import com.loh.domain.items.equipables.armors.base.BaseArmor;
import com.loh.shared.Bonuses;
import com.loh.shared.Properties;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.ManyToOne;

@Entity
public class ArmorModel extends EquipableTemplate {

	@Getter @Setter
	protected ItemTemplateType itemTemplateType = ItemTemplateType.Armor;

	public ArmorModel(){
		super();
		baseArmor = new BaseArmor();
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
		return baseArmor != null ? baseArmor.getCategory().getDodge() + Bonuses.GetBonus(bonuses, Properties.Dodge) : 0;
	}

}
