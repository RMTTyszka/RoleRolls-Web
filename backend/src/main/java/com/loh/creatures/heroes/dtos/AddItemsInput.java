package com.loh.creatures.heroes.dtos;

import com.loh.items.itemInstance.ItemInstance;

import java.util.List;
import java.util.UUID;

public class AddItemsInput {
    public List<ItemInstance> items;
    public UUID heroId;
}
