package com.rolerolls.domain.items.equipables.rings;

import java.util.Arrays;
import java.util.HashSet;
import java.util.Set;

public class DefaultRings {
    public static String Ring = "Ring";
    public static String DummyRing = "Dummy Ring";
    public static String NoRing = "No Ring";

    public static Set<String> getList() {
        return new HashSet<>(Arrays.asList(Ring));
    }
}
