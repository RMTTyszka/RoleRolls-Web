package com.rolerolls.application.creatures.heroes.dtos;

import com.rolerolls.domain.items.instances.ItemInstance;

import java.util.List;
import java.util.UUID;

public class AddItemsInput {
    public List<ItemInstance> items;
    public UUID heroId;
}
