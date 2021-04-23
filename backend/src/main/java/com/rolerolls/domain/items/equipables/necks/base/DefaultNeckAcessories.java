package com.rolerolls.domain.items.equipables.necks.base;

import java.util.Arrays;
import java.util.HashSet;
import java.util.Set;

public class DefaultNeckAcessories {
    public static String Necklace = "Necklace";
    public static String Colar = "Colar";
    public static String Cape = "Cape";
    public static String LeatherShoulders = "Leather Shoulders";
    public static String DummyNeckAcessory = "Dummy Neck Accessory";
    public static String NoNeckAcessory = "No Neck Accessory";

    public static Set<String> getList() {
        return new HashSet<>(Arrays.asList(Necklace, Colar, Cape, LeatherShoulders));
    }
}
