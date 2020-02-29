package com.loh.creatures.heroes.equipment;

import com.loh.items.*;
import com.loh.items.armors.ArmorInstance;
import com.loh.items.weapons.WeaponInstance;
import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.OneToOne;
import javax.persistence.Transient;
import java.util.Arrays;
import java.util.List;

@javax.persistence.Entity
public class Equipment extends Entity {

	public Equipment(){
		armor = new ArmorInstance();
/*		mainWeapon = new Weapon();
		offWeapon = new Weapon();*/
	}
	
	@OneToOne @Getter @Setter
	private ArmorInstance armor = new ArmorInstance();

	@OneToOne @Getter @Setter
	private WeaponInstance mainWeapon;
	
	@OneToOne @Getter @Setter
	private WeaponInstance offWeapon;

	public Integer getDefense() {
		return armor.getArmorModel().getDefense();
	}
	public Integer getEvasion() {
		return armor.getArmorModel().getEvasion();
	}
	@Transient
	public List<EquipableInstance> listOfEquipment() {

		return Arrays.asList(this.mainWeapon, this.armor);
	}

}
