package com.loh.creatures.heroes.equipment;

import com.loh.items.weapons.weaponCategory.WeaponType;
import com.loh.items.weapons.weaponInstance.WeaponInstance;

public class GripService {

    public static boolean validateGripType(WeaponInstance weapon, GripType gripType) {
        if (isBareHanded(weapon)) return true;
        if (gripType == null) return true;
        if (isHeavyWeapon(weapon)) return gripType == GripType.TwoHandedHeavyWeapon || gripType == GripType.OneHandedHeavyWeapon;
        if (isMediumWeapon(weapon)) return gripType == GripType.OneMediumWeapon || gripType == GripType.TwoHandedMediumWeapon || gripType == GripType.TwoWeaponsMedium;
        if (isLightWeapon(weapon)) return gripType == GripType.OneLightWeapon || gripType == GripType.TwoWeaponsLight;
        return false;
    }

    private static boolean isBareHanded(WeaponInstance weapon) {
        return weapon != null && weapon.getWeaponModel().getBaseWeapon().getCategory().getWeaponType() == WeaponType.None;
    }
    private static boolean isHeavyWeapon(WeaponInstance weapon) {
        return weapon != null && weapon.getWeaponModel().getBaseWeapon().getCategory().getWeaponType() == WeaponType.Heavy;
    }
    private static boolean isMediumWeapon(WeaponInstance weapon) {
        return weapon != null && weapon.getWeaponModel().getBaseWeapon().getCategory().getWeaponType() == WeaponType.Medium;
    }
    private static boolean isLightWeapon(WeaponInstance weapon) {
        return weapon != null && weapon.getWeaponModel().getBaseWeapon().getCategory().getWeaponType() == WeaponType.Light;
    }

    public static CorrectedGripType getCorrectedGripType(WeaponInstance newWeapon, WeaponInstance offWeapon, GripType gripType) throws Exception {
        if (isBareHanded(newWeapon)) return new CorrectedGripType(gripType);
        if (isLightWeapon(newWeapon)) {
            if (isLightWeapon(offWeapon) || isMediumWeapon(offWeapon)) {
                return new CorrectedGripType(GripType.TwoWeaponsLight);
            }
            return new CorrectedGripType(GripType.OneLightWeapon);
        }
        if (isMediumWeapon(newWeapon)) {
            if (isMediumWeapon(offWeapon) || isLightWeapon(offWeapon)) {
                return new CorrectedGripType(GripType.TwoWeaponsMedium);
            }
            if (gripType == GripType.TwoHandedMediumWeapon) {
                return new CorrectedGripType(GripType.TwoHandedMediumWeapon, true);
            }
            return new CorrectedGripType(GripType.OneMediumWeapon, false);
        }
        if (isHeavyWeapon(newWeapon)) {
            if (gripType == GripType.TwoHandedHeavyWeapon || gripType == GripType.OneHandedHeavyWeapon) {
                return new CorrectedGripType(gripType, offWeapon.getWeaponModel().getBaseWeapon().getCategory().getWeaponType() != WeaponType.Shield);
            }
        }

        throw new Exception("GridType and Weapon are not compatible");

    }
}
