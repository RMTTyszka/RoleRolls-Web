package com.loh.creatures.heroes.equipment;

public class CorrectedGripType {
    public CorrectedGripType(GripType gripType, boolean shouldUnequipOffWeapon) {
        this.gripType = gripType;
        this.shouldUnequipOffWeapon = shouldUnequipOffWeapon;
    }
    public CorrectedGripType(GripType gripType) {
        this.gripType = gripType;
        this.shouldUnequipOffWeapon = false;
    }

    public GripType gripType;
    public boolean shouldUnequipOffWeapon;
}
