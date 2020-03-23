package com.loh.items.equipable.belts.baseBelts;

import java.util.Arrays;
import java.util.HashSet;
import java.util.Set;

public class DefaultBelts {
    public static String Sash = "Sash";
    public static String Belt = "belt";
    public static String DummyBelt = "Dummy Belt";
    public static String NoBelt = "No Belt";

    public static Set<String> getList() {
        return new HashSet<>(Arrays.asList(Sash, Belt));
    }
}
