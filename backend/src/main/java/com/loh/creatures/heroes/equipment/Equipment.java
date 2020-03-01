package com.loh.creatures.heroes.equipment;

import com.loh.items.EquipableInstance;
import com.loh.items.armors.ArmorInstance;
import com.loh.items.weapons.weaponInstance.WeaponInstance;
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

	@Getter
	private GripType mainWeaponGripType;
	@Getter
	private GripType offWeaponGridType;

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

	public void equipMainWeapon(WeaponInstance weapon, GripType gripType) throws Exception {
		this.setMainWeapon(weapon);
		if (gripType != null) {
			this.setMainWeaponGripType(gripType);
		}
	}
	public void equipOffWeapon(WeaponInstance weapon, GripType gripType) throws Exception {
		this.setOffWeapon(weapon);
		if (gripType != null) {
			this.setOffWeaponGripType(gripType);
		}
	}

	private void setMainWeaponGripType(GripType gripType) throws Exception {
		if (GripService.validateGripType(this.getMainWeapon(), gripType)) {
			CorrectedGripType correctedGripType = GripService.getCorrectedGripType(this.getMainWeapon(), this.getOffWeapon(), gripType);
			if (correctedGripType.shouldUnequipOffWeapon) {
				this.quicklyUnequipOffWeapon();
			}
			this.mainWeaponGripType = correctedGripType.gripType;
		};
	}
	private void setOffWeaponGripType(GripType gripType) throws Exception {
		if (GripService.validateGripType(this.getOffWeapon(), gripType)) {
			CorrectedGripType correctedGripType = GripService.getCorrectedGripType(this.getOffWeapon(), this.getMainWeapon(), gripType);
			if (correctedGripType.shouldUnequipOffWeapon) {
				this.quicklyUnequipMainWeapon();
			}
			this.offWeaponGridType = correctedGripType.gripType;
		};
	}
	private void quicklyUnequipOffWeapon() {
		this.setOffWeapon(null);
	}
	private void quicklyUnequipMainWeapon() {
		this.setMainWeapon(null);
	}

}
