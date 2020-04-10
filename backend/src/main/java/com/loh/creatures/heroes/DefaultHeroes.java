package com.loh.creatures.heroes;

public class DefaultHeroes {

    public static String OneLightWeapon = "One Light Weapon Dummy";
    public static String OneMediumWeapon = "One Medium Weapon Dummy";
    public static String OneHeavyWeapon = "One Heavy Weapon Dummy";
    public static String TwoLightWeapons = "Two Light Weapons Dummy";
    public static String TwoMediumWeapons = "Two Medium Weapons Dummy";
    public static String LightArmor= "Light Armor Dummy";
    public static String MediumArmor = "Medium Armor Dummy";
    public static String HeavyArmor = "Heavy Armor Dummy";

    public static String getNameWithLevel(String heroName, Integer level) {
        return heroName + " level " + level;
    }
}
