package com.loh.creatures.heroes.dtos;

import com.loh.items.itemInstance.ItemInstance;
import com.loh.shops.ShopItem;

public class BuyItemOutput {
    ItemInstance itemInstance;
    ShopItem shopItem;

    public BuyItemOutput() {
    }

    public BuyItemOutput(ItemInstance itemInstance, ShopItem item) {
        this.itemInstance = itemInstance;
        this.shopItem = item;
    }
}
