package com.loh.creatures.heroes.equipment;

import com.loh.items.equipable.armors.armorInstance.ArmorInstance;
import com.loh.items.equipable.belts.beltInstances.BeltInstance;
import com.loh.items.equipable.gloves.gloveInstances.GloveInstance;
import com.loh.items.equipable.head.headpieceInstances.HeadpieceInstance;
import com.loh.items.equipable.neck.neckAccessoryInstances.NeckAccessoryInstance;
import com.loh.items.equipable.rings.head.ringInstances.RingInstance;
import com.loh.items.equipable.weapons.weaponInstance.WeaponInstance;
import com.loh.shared.Bonuses;
import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.OneToOne;

@javax.persistence.Entity
public class Equipment extends Entity {

	public Equipment(){
	}
	public Integer getBonusLevel(String property) {
		Integer armorBonus = Bonuses.GetEquipmentBonusLevel(armor.getBonuses(), property);
		Integer weaponBonus = mainWeapon != null ? Bonuses.GetEquipmentBonusLevel(mainWeapon.getBonuses(), property) : 0;
		Integer offWeaponBonus = offWeapon != null ? Bonuses.GetEquipmentBonusLevel(offWeapon.getBonuses(), property) : 0;
		Integer beltBonus = Bonuses.GetEquipmentBonusLevel(belt.getBonuses(), property);
		Integer gloveBonus = Bonuses.GetEquipmentBonusLevel(gloves.getBonuses(), property);
		Integer headBonus = Bonuses.GetEquipmentBonusLevel(headpiece.getBonuses(), property);
		Integer neckBonus = Bonuses.GetEquipmentBonusLevel(neckAccessory.getBonuses(), property);
		Integer ringRightBonus = Bonuses.GetEquipmentBonusLevel(ringRight.getBonuses(), property);
		Integer ringLeftBonus = Bonuses.GetEquipmentBonusLevel(ringLeft.getBonuses(), property);
		return armorBonus + weaponBonus + offWeaponBonus + beltBonus + gloveBonus + headBonus + neckBonus + ringLeftBonus + ringRightBonus;
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
	@OneToOne @Getter @Setter
	private HeadpieceInstance headpiece;
	@OneToOne @Getter @Setter
	private NeckAccessoryInstance neckAccessory;
	@OneToOne @Getter @Setter
	private RingInstance ringRight;
	@OneToOne @Getter @Setter
	private RingInstance ringLeft;

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
	public void equipHeadpiece(HeadpieceInstance headpiece) {
		this.setHeadpiece(headpiece);
	}
	public void equipNeckAccessory(NeckAccessoryInstance neckAccessory) {
		this.setNeckAccessory(neckAccessory);
	}
	public void equipRingRight(RingInstance ringInstance) {
		this.setRingRight(ringInstance);
	}
	public void equipRingLeft(RingInstance ringInstance) {
		this.setRingLeft(ringInstance);
	}

	private void setMainWeaponGripType(GripType gripType) throws Exception {
		if (GripService.validateGripType(getMainWeapon(), gripType)) {
			CorrectedGripType correctedGripType = GripService.getCorrectedGripType(getMainWeapon(), getOffWeapon(), gripType, getOffWeaponGridType());
			if (correctedGripType.shouldUnequipOffWeapon) {
				this.quicklyUnequipOffWeapon();
			}
			this.mainWeaponGripType = correctedGripType.gripType;
			this.offWeaponGridType = correctedGripType.offWeaponGripType;
		}
	}
	private void setOffWeaponGripType(GripType gripType) throws Exception {
		if (GripService.validateGripType(getOffWeapon(), gripType)) {
			CorrectedGripType correctedGripType = GripService.getCorrectedGripType(getOffWeapon(), getMainWeapon(), gripType, getMainWeaponGripType());
			if (correctedGripType.shouldUnequipOffWeapon) {
				this.quicklyUnequipMainWeapon();
			}
			this.offWeaponGridType = correctedGripType.gripType;
			this.mainWeaponGripType = correctedGripType.offWeaponGripType;
		};
	}
	private void quicklyUnequipOffWeapon() {
		this.setOffWeapon(null);
	}
	private void quicklyUnequipMainWeapon() {
		this.setMainWeapon(null);
	}

}
