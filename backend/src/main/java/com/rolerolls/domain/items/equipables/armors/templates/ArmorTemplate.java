package com.rolerolls.domain.items.equipables.armors.templates;

import com.rolerolls.domain.items.templates.ItemTemplateType;
import com.rolerolls.domain.items.equipables.EquipableSlot;
import com.rolerolls.domain.items.equipables.EquipableTemplate;
import com.rolerolls.domain.items.equipables.armors.base.BaseArmor;
import com.rolerolls.shared.Bonuses;
import com.rolerolls.shared.Properties;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.ManyToOne;

@Entity
public class ArmorTemplate extends EquipableTemplate {

	public ArmorTemplate(){
		super();
		baseArmor = new BaseArmor();
	}

	public ArmorTemplate(String name, BaseArmor baseArmor) {
		this.baseArmor = baseArmor;
		this.name = name;
	}

	@Override
	public ItemTemplateType getItemTemplateType() {
		return ItemTemplateType.Armor;
	}

	@Override
	public EquipableSlot getSlot() {
		return EquipableSlot.Chest;
	};

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
