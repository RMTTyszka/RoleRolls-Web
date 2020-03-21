package com.loh.creatures.heroes.equipment;

public class CorrectedGripType {
    public CorrectedGripType(GripType gripType, boolean shouldUnequipOffWeapon, GripType offWeaponRipType) {
        this.gripType = gripType;
        this.offWeaponRipType = offWeaponRipType;
        this.shouldUnequipOffWeapon = shouldUnequipOffWeapon;
    }
    public CorrectedGripType(GripType gripType) {
        this.gripType = gripType;
        this.shouldUnequipOffWeapon = false;
    }

    public GripType gripType;
    public GripType offWeaponRipType;
    public boolean shouldUnequipOffWeapon;
}
