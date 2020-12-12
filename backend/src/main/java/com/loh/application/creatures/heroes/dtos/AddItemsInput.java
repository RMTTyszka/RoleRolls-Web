package com.loh.application.creatures.heroes.dtos;

import com.loh.domain.items.instances.ItemInstance;

import java.util.List;
import java.util.UUID;

public class AddItemsInput {
    public List<ItemInstance> items;
    public UUID heroId;
}
