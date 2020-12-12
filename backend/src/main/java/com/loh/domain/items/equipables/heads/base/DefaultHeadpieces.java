package com.loh.domain.items.equipables.heads.base;

import java.util.Arrays;
import java.util.HashSet;
import java.util.Set;

public class DefaultHeadpieces {
    public static String Cap = "Cap";
    public static String Helmet = "Helmet";
    public static String Hat = "Hat";
    public static String DummyHeadpiece = "Dummy Headpiece";
    public static String NoHeadpiece = "No Headpiece";

    public static Set<String> getList() {
        return new HashSet<>(Arrays.asList(Cap, Helmet, Hat));
    }
}
