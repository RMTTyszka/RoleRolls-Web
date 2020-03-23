package com.loh.items.equipable.armors.armorCategories;

import lombok.Getter;

import java.util.List;
import java.util.stream.Collectors;

public class ArmorCategoryDto {

    public ArmorCategoryDto(ArmorCategory armorCategory) {
        name = armorCategory.name();
        defense = armorCategory.getDefense();
        dodge = armorCategory.getDodge();
        baseDefense = armorCategory.getBaseDefense();
    }

    @Getter
    private String name;
    @Getter
    private int defense;
    @Getter
    private int dodge;
    @Getter
    private int baseDefense;

    public static List<ArmorCategoryDto> getList() {
        return ArmorCategory.getList().stream().map(armorCategory -> new ArmorCategoryDto(armorCategory)).collect(Collectors.toList());
    }
}
