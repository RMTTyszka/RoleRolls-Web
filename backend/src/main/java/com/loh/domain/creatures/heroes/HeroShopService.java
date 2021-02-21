package com.loh.domain.creatures.heroes;

import com.loh.application.creatures.heroes.dtos.BuyItemOutput;
import com.loh.domain.items.instances.ItemInstance;
import com.loh.domain.items.instances.ItemInstantiator;
import com.loh.domain.shops.Shop;
import com.loh.domain.shops.ShopItem;
import com.loh.domain.shops.ShopRepository;
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
            ItemInstance itemInstance = itemInstantiator.instantiate(item.getItem(), item.getLevel(), quantity, true);
            hero.getInventory().addItem(itemInstance);
            hero.getInventory().removeFunds(item.getValue() * quantity);
            item.removeQuantity(quantity);
            heroRepository.save(hero);
            shopRepository.save(shop);

            BuyItemOutput output = new BuyItemOutput(itemInstance, item);
            return output;
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
