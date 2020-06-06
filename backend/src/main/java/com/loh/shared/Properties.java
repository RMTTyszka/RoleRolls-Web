package com.loh.shared;

import com.loh.creatures.Attributes;

import java.util.Arrays;

public class Properties {
    public static String Hit = "Hit";
    public static String Defense = "Defense";
    public static String Evasion = "Evasion";
    public static String Dodge = "Dodge";
    public static String Resistance = "Resistance";
    public static String SpecialAttack = "Special Attack";
    public static String MagicDefense = "Magic Defense";
    public static java.util.List<String> List = Arrays.asList(
            Defense,
            Evasion,
            Dodge,
            Resistance,
            SpecialAttack,
            MagicDefense,
            Attributes.Strength,
            Attributes.Agility,
            Attributes.Vitality,
            Attributes.Wisdom,
            Attributes.Intuition,
            Attributes.Charisma
            );
}
