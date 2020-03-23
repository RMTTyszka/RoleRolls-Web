package com.loh.creatures.heroes.equipment;

import com.loh.items.EquipableInstance;
import com.loh.items.equipable.armors.armorInstance.ArmorInstance;
import com.loh.items.equipable.belts.beltInstances.BeltInstance;
import com.loh.items.equipable.gloves.gloveInstances.GloveInstance;
import com.loh.items.equipable.weapons.weaponInstance.WeaponInstance;
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
	}
	public Integer getBonusLevel(String property) {
		Integer armorBonus = armor.getBonusLevel(property);
		Integer weaponBonus = mainWeapon.getBonusLevel(property);
		Integer offWeaponBonus = offWeapon != null ? offWeapon.getBonusLevel(property) : 0;

		return armorBonus + weaponBonus + offWeaponBonus;
	}
	
	@OneToOne @Getter @Setter
	private ArmorInstance armor;

	@OneToOne @Getter @Setter
	private WeaponInstance mainWeapon;
	
	@OneToOne @Getter @Setter
	private WeaponInstance offWeapon;
	@OneToOne @Getter @Setter
	private GloveInstance gloves;
	@OneToOne @Getter @Setter
	private BeltInstance belt;

	@Getter
	private GripType mainWeaponGripType;
	@Getter
	private GripType offWeaponGridType;

	public Integer getDefense() {
		return armor.getDefense();
	}
	public Integer getEvasion() {
		return armor.getEvasion();
	}
	public Integer getDodge() {
		return armor.getDodge();
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
	public void equipArmor(ArmorInstance armor) {
		this.setArmor(armor);
	}
	public void equipGloves(GloveInstance gloves) {
		this.setGloves(gloves);
	}
	public void equipBelt(BeltInstance belt) {
		this.setBelt(belt);
	}

	private void setMainWeaponGripType(GripType gripType) throws Exception {
		if (GripService.validateGripType(this.getMainWeapon(), gripType)) {
			CorrectedGripType correctedGripType = GripService.getCorrectedGripType(this.getMainWeapon(), this.getOffWeapon(), gripType);
			if (correctedGripType.shouldUnequipOffWeapon) {
				this.quicklyUnequipOffWeapon();
			}
			this.mainWeaponGripType = correctedGripType.gripType;
			this.offWeaponGridType = correctedGripType.offWeaponRipType;
		}
	}
	private void setOffWeaponGripType(GripType gripType) throws Exception {
		if (GripService.validateGripType(this.getOffWeapon(), gripType)) {
			CorrectedGripType correctedGripType = GripService.getCorrectedGripType(this.getOffWeapon(), this.getMainWeapon(), gripType);
			if (correctedGripType.shouldUnequipOffWeapon) {
				this.quicklyUnequipMainWeapon();
			}
			this.offWeaponGridType = correctedGripType.gripType;
			this.mainWeaponGripType = correctedGripType.offWeaponRipType;
		};
	}
	private void quicklyUnequipOffWeapon() {
		this.setOffWeapon(null);
	}
	private void quicklyUnequipMainWeapon() {
		this.setMainWeapon(null);
	}

}
