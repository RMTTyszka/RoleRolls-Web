package com.rolerolls.domain.creatures.monsters;

public class DefaultMonsters {

    public static String OneLightWeapon = "One Light Weapon Dummy Monster";
    public static String OneMediumWeapon = "One Medium Weapon Dummy Monster";
    public static String OneHeavyWeapon = "One Heavy Weapon Dummy Monster";
    public static String TwoLightWeapons = "Two Light Weapons Dummy Monster";
    public static String TwoMediumWeapons = "Two Medium Weapons Dummy Monster";
    public static String LightArmor= "Light Armor Dummy Monster";
    public static String MediumArmor = "Medium Armor Dummy Monster";
    public static String HeavyArmor = "Heavy Armor Dummy Monster";

    public static String getNameWithLevel(String heroName, Integer level) {
        return heroName + " level " + level;
    }
}
