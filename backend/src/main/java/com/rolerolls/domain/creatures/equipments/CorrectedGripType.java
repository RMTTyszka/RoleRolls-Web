package com.rolerolls.domain.creatures.equipments;

public class CorrectedGripType {
    public CorrectedGripType(GripType gripType, boolean shouldUnequipOffWeapon, GripType offWeaponGripType) {
        this.gripType = gripType;
        this.offWeaponGripType = offWeaponGripType;
        this.shouldUnequipOffWeapon = shouldUnequipOffWeapon;
    }
    public CorrectedGripType(GripType gripType) {
        this.gripType = gripType;
        this.shouldUnequipOffWeapon = false;
    }

    public GripType gripType;
    public GripType offWeaponGripType;
    public boolean shouldUnequipOffWeapon;
}
