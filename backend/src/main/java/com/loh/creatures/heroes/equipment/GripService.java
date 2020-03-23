package com.loh.creatures.heroes.equipment;

import com.loh.items.equipable.weapons.weaponCategory.WeaponCategory;
import com.loh.items.equipable.weapons.weaponInstance.WeaponInstance;

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
        return weapon != null && weapon.getWeaponModel().getBaseWeapon().getCategory() == WeaponCategory.None;
    }
    private static boolean isHeavyWeapon(WeaponInstance weapon) {
        return weapon != null && weapon.getWeaponModel().getBaseWeapon().getCategory() == WeaponCategory.Heavy;
    }
    private static boolean isMediumWeapon(WeaponInstance weapon) {
        return weapon != null && weapon.getWeaponModel().getBaseWeapon().getCategory() == WeaponCategory.Medium;
    }
    private static boolean isLightWeapon(WeaponInstance weapon) {
        return weapon != null && weapon.getWeaponModel().getBaseWeapon().getCategory() == WeaponCategory.Light;
    }

    public static CorrectedGripType getCorrectedGripType(WeaponInstance newWeapon, WeaponInstance offWeapon, GripType gripType) throws Exception {
        if (isBareHanded(newWeapon)) return new CorrectedGripType(gripType);
        if (isLightWeapon(newWeapon)) {
            if (isLightWeapon(offWeapon)) {
                return new CorrectedGripType(GripType.TwoWeaponsLight, false, GripType.TwoWeaponsLight);
            } else if (isMediumWeapon(offWeapon)) {
                return new CorrectedGripType(GripType.TwoWeaponsLight, false, GripType.TwoWeaponsMedium);
            }
            return new CorrectedGripType(GripType.OneLightWeapon);
        }
        if (isMediumWeapon(newWeapon)) {
            if (isMediumWeapon(offWeapon)) {
                return new CorrectedGripType(GripType.TwoWeaponsMedium, false, GripType.TwoWeaponsMedium);
            } else if (isLightWeapon(offWeapon)) {
                return new CorrectedGripType(GripType.TwoWeaponsMedium, false, GripType.TwoWeaponsLight);
            }
            if (gripType == GripType.TwoHandedMediumWeapon) {
                return new CorrectedGripType(GripType.TwoHandedMediumWeapon, true, null);
            }
            return new CorrectedGripType(GripType.OneMediumWeapon, false, null);
        }
        if (isHeavyWeapon(newWeapon)) {
            if ((gripType == GripType.TwoHandedHeavyWeapon || gripType == GripType.OneHandedHeavyWeapon) && offWeapon != null) {
                return new CorrectedGripType(gripType, offWeapon.getWeaponModel().getBaseWeapon().getCategory() != WeaponCategory.Shield, null);
            }
            return new CorrectedGripType(gripType, false, null);
        }

        throw new Exception("GridType and Weapon are not compatible");

    }
}
