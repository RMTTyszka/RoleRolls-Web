package com.rolerolls.application.creatures.heroes.dtos;

import com.rolerolls.domain.items.instances.ItemInstance;
import com.rolerolls.domain.shops.ShopItem;

public class BuyItemOutput {
    public ItemInstance itemInstance;
    public ShopItem shopItem;

    public BuyItemOutput() {
    }

    public BuyItemOutput(ItemInstance itemInstance, ShopItem item) {
        this.itemInstance = itemInstance;
        this.shopItem = item;
    }
}
