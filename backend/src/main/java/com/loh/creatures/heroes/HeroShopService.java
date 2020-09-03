package com.loh.creatures.heroes;

import com.loh.creatures.heroes.dtos.BuyItemOutput;
import com.loh.items.itemInstance.ItemInstance;
import com.loh.items.itemInstance.ItemInstantiator;
import com.loh.shops.Shop;
import com.loh.shops.ShopItem;
import com.loh.shops.ShopRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Service
public class HeroShopService {

    @Autowired
    ShopRepository shopRepository;
    @Autowired
    HeroRepository heroRepository;
    @Autowired
    ItemInstantiator itemInstantiator;

    public BuyItemOutput buy(UUID heroId, UUID shopId, UUID itemId, Integer quantity) {
        Shop shop = shopRepository.findById(shopId).get();
        ShopItem item = shop.getItems().stream().filter(i -> i.getId().equals(itemId)).findFirst().get();
        Hero hero = heroRepository.findById(heroId).get();

        if (hasEnoughMoney(hero, item, quantity) && hasEnoughQuantity(item, quantity)) {
            ItemInstance itemInstance = itemInstantiator.instantiate(item.getItem(), item.getLevel(), true);
            hero.getInventory().addItem(itemInstance);
            hero.getInventory().removeFunds(item.getValue() * quantity);
            item.removeQuantity(quantity);
            heroRepository.save(hero);
            shopRepository.save(shop);

            BuyItemOutput output = new BuyItemOutput(itemInstance, item);
        }


        return new BuyItemOutput();
    }

    private boolean hasEnoughQuantity(ShopItem item, Integer quantity) {
        return item.getQuantity() >= quantity;
    }

    private boolean hasEnoughMoney(Hero hero, ShopItem item, Integer quantity) {
        return hero.getInventory().getCash1() >= item.getValue() * quantity;
    }
}
