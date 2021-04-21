package com.loh.domain.creatures.equipments;

import com.loh.domain.items.equipables.armors.instances.ArmorInstance;
import com.loh.domain.items.equipables.belts.instances.BeltInstance;
import com.loh.domain.items.equipables.gloves.instances.GloveInstance;
import com.loh.domain.items.equipables.heads.instances.HeadpieceInstance;
import com.loh.domain.items.equipables.necks.instances.NeckAccessoryInstance;
import com.loh.domain.items.equipables.rings.instances.RingInstance;
import com.loh.domain.items.equipables.weapons.instances.WeaponInstance;
import com.loh.shared.Bonuses;
import com.loh.shared.Entity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.OneToOne;

@javax.persistence.Entity
public class Equipment extends Entity {

	public Equipment(){
		this.armor = new ArmorInstance();
		this.mainWeapon = new WeaponInstance();
		this.offWeapon = null;
		this.belt = new BeltInstance();
		this.headpiece = new HeadpieceInstance();
		this.neckAccessory = new NeckAccessoryInstance();
		this.ringLeft = new RingInstance();
		this.ringRight = new RingInstance();
		this.gloves = new GloveInstance();
		this.mainWeaponGripType = GripType.OneMediumWeapon;
		this.offWeaponGripType = GripType.None;
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

	public Integer getBonus(String property) {
		Integer armorBonus = Bonuses.GetEquipmentBonus(armor.getBonuses(), property);
		Integer weaponBonus = mainWeapon != null ? Bonuses.GetEquipmentBonus(mainWeapon.getBonuses(), property) : 0;
		Integer offWeaponBonus = offWeapon != null ? Bonuses.GetEquipmentBonus(offWeapon.getBonuses(), property) : 0;
		Integer beltBonus = Bonuses.GetEquipmentBonus(belt.getBonuses(), property);
		Integer gloveBonus = Bonuses.GetEquipmentBonus(gloves.getBonuses(), property);
		Integer headBonus = Bonuses.GetEquipmentBonus(headpiece.getBonuses(), property);
		Integer neckBonus = Bonuses.GetEquipmentBonus(neckAccessory.getBonuses(), property);
		Integer ringRightBonus = Bonuses.GetEquipmentBonus(ringRight.getBonuses(), property);
		Integer ringLeftBonus = Bonuses.GetEquipmentBonus(ringLeft.getBonuses(), property);
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
	private GripType offWeaponGripType;

	public Integer getDefense() {
		return armor.getDefense();
	}
	public Integer getEvasion() {
		Integer armorEvasion = armor.getEvasion();
		Integer mainShieldEvasion = mainWeaponGripType.getShieldEvasionBonus();
		Integer offShieldEvasion = offWeaponGripType.getShieldEvasionBonus();
		return armorEvasion + mainShieldEvasion + offShieldEvasion;
	}
	public Integer getDodge() {
		return armor != null ? armor.getDodge() : 0;
	}

	public WeaponInstance equipMainWeapon(WeaponInstance weapon, GripType gripType) throws Exception {
		WeaponInstance previousWeapon = this.getMainWeapon();
		this.setMainWeapon(weapon);
		this.setMainWeaponGripType(gripType);
		return previousWeapon;
	}
	public WeaponInstance equipOffWeapon(WeaponInstance weapon, GripType gripType) {
		WeaponInstance previousWeapon = this.getOffWeapon();
		this.setOffWeapon(weapon);
		this.setOffWeaponGripType(gripType);
		return previousWeapon;
	}
	public ArmorInstance equipArmor(ArmorInstance armor) {
		ArmorInstance previousArmor = this.getArmor();
		this.setArmor(armor);
		return previousArmor;
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
/*		if (GripService.validateGripType(getMainWeapon(), gripType)) {
			CorrectedGripType correctedGripType = GripService.getCorrectedGripType(getMainWeapon(), getOffWeapon(), gripType, getOffWeaponGripType());
			if (correctedGripType.shouldUnequipOffWeapon) {
				this.quicklyUnequipOffWeapon();
			}*/
		this.mainWeaponGripType = gripType;
		GripType offWeaponGripType = GripType.getGripType(getOffWeaponGripType(), gripType);
		this.offWeaponGripType = offWeaponGripType;
	}
	private void setOffWeaponGripType(GripType gripType) {
/*		if (GripService.validateGripType(getOffWeapon(), gripType)) {
			CorrectedGripType correctedGripType = GripService.getCorrectedGripType(getOffWeapon(), getMainWeapon(), gripType, getMainWeaponGripType());
			if (correctedGripType.shouldUnequipOffWeapon) {
				this.quicklyUnequipMainWeapon();
			}*/
		this.offWeaponGripType = gripType;
		GripType mainWeaponGripType = GripType.getGripType( getMainWeaponGripType(), gripType);
		this.mainWeaponGripType = mainWeaponGripType;
	}
	private void quicklyUnequipOffWeapon() {
		this.setOffWeapon(null);
	}
	private void quicklyUnequipMainWeapon() {
		this.setMainWeapon(null);
	}

}
