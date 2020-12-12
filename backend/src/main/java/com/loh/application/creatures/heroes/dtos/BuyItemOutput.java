package com.loh.application.creatures.heroes.dtos;

import com.loh.domain.items.instances.ItemInstance;
import com.loh.domain.shops.ShopItem;

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
