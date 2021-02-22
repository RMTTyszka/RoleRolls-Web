package com.loh.domain.items.equipables.armors;

public class DefaultArmors {

    public static DefaultArmor[] lightArmors = new DefaultArmor[] {
            new DefaultArmor("Leather Armor", "", new String[]{}, new String[]{}),
            new DefaultArmor("Cloth Armor", "", new String[]{}, new String[]{}),
            new DefaultArmor("Brigandine", "", new String[]{}, new String[]{}),
    };
    public static DefaultArmor[] mediumArmors = new DefaultArmor[] {
            new DefaultArmor("Chain Mail", "", new String[]{}, new String[]{}),
            new DefaultArmor("Half Plate", "", new String[]{}, new String[]{}),
            new DefaultArmor("Fur Armor", "", new String[]{}, new String[]{}),
    };
    public static DefaultArmor[] heavyArmors = new DefaultArmor[] {
            new DefaultArmor("Full Plate", "", new String[]{}, new String[]{}),
            new DefaultArmor("Scale Armor", "", new String[]{}, new String[]{}),
            new DefaultArmor("Heavy Fur Armor", "", new String[]{}, new String[]{}),
    };

    public static String dummyLightArmor = "Dummy Light Armor";
    public static String dummyMediumArmor = "Dummy Medium Armor";
    public static String dummyHeavyArmor = "Dummy Heavy Armor";
    public static String dummyNoneArmor = "Dummy None Armor";

    public static String NoneArmor = "None Armor";
}

