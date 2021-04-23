package com.rolerolls.domain.items.equipables.gloves.base;

import java.util.Arrays;
import java.util.HashSet;
import java.util.Set;

public class DefaultGloves {
    public static String LeatherGlove = "Leather Glove";
    public static String ClothGlove = "Cloth Glove";
    public static String Gauntled = "Gauntled";
    public static String DummyGloves = "Dummy Gloves";
    public static String NoGloves = "No Gloves";

    public static Set<String> getList() {
        return new HashSet<>(Arrays.asList(LeatherGlove, ClothGlove, Gauntled));
    }
}
