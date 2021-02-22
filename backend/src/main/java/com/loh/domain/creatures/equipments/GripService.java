package com.loh.domain.creatures.equipments;

import com.loh.domain.items.equipables.weapons.categories.WeaponCategory;
import com.loh.domain.items.equipables.weapons.instances.WeaponInstance;

public class GripService {

    public static boolean validateGripType(WeaponInstance weaponInstance, GripType gripType) {
        if (isBareHanded(weaponInstance)) return true;
        if (gripType == null) return true;
        if (isHeavyWeapon(weaponInstance)) return gripType == GripType.TwoHandedHeavyWeapon || gripType == GripType.OneHandedHeavyWeapon;
        if (isMediumWeapon(weaponInstance)) return gripType == GripType.OneMediumWeapon || gripType == GripType.TwoHandedMediumWeapon || gripType == GripType.TwoWeaponsMedium;
        if (isLightWeapon(weaponInstance)) return gripType == GripType.OneLightWeapon || gripType == GripType.TwoWeaponsLight;
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
    private static boolean isShield(WeaponInstance weapon) {
        WeaponCategory weaponCategory = weapon.getWeaponModel().getBaseWeapon().getCategory();
        return weapon != null &&
                (weaponCategory == WeaponCategory.LightShield
                || weaponCategory == WeaponCategory.MediumShield
                || weaponCategory == WeaponCategory.HeavyShield);
    }

    public static CorrectedGripType getCorrectedGripType(WeaponInstance newWeapon, WeaponInstance offWeapon, GripType gripType, GripType offWeaponGripType) throws Exception {
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
            return new CorrectedGripType(GripType.OneMediumWeapon, false, offWeaponGripType);
        }
        if (isHeavyWeapon(newWeapon)) {
            if (gripType == GripType.TwoHandedHeavyWeapon && offWeapon != null) {
                return new CorrectedGripType(gripType, true, null);
            }
            if (gripType == GripType.OneHandedHeavyWeapon && offWeapon != null) {
                return new CorrectedGripType(gripType, !isShield(offWeapon), offWeaponGripType);
            }
            return new CorrectedGripType(gripType, false, offWeaponGripType);
        }

        throw new Exception("GridType and Weapon are not compatible");

    }
}
