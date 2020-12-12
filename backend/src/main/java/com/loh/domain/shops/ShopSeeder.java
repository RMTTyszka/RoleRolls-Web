package com.loh.domain.shops;

import com.loh.domain.items.equipables.armors.DefaultArmors;
import com.loh.domain.items.equipables.armors.instances.ArmorInstanceRepository;
import com.loh.domain.items.equipables.armors.models.ArmorModel;
import com.loh.domain.items.equipables.armors.models.ArmorModelRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class ShopSeeder {

    @Autowired
    ShopRepository shopRepository;
    @Autowired
    ArmorInstanceRepository armorInstanceRepository;
    @Autowired
    ArmorModelRepository armorModelRepository;
    @Autowired
    ShopArmorRepository shopArmorRepository;

    public void seed() {
        Shop shop = shopRepository.findBySystemDefaultAndName(true, ShopTokens.HeroCreationShopName);
        if (shop == null) {
            shop = new Shop();
            shop.setName("HeroCreateShop");
            shop.setSystemDefault(true);
        }
        for (String armorName: DefaultArmors.lightArmors) {
            AddArmor(shop, armorName, 10);
        }
        for (String armorName: DefaultArmors.mediumArmors) {
            AddArmor(shop, armorName, 20);
        }
        for (String armorName: DefaultArmors.heavyArmors) {
            AddArmor(shop, armorName, 30);
        }
        shopRepository.save(shop);
    }

    private void AddArmor(Shop shop, String armorName, int value) {
        if (!shop.getItems().stream().filter(a -> a.getItem().getName().equals("Common " + armorName)).findFirst().isPresent()) {
            ArmorModel armorModel = armorModelRepository.findByNameAndSystemDefaultTrue("Common " + armorName);
            ShopArmor shopArmor = new ShopArmor(armorModel, 33, armorModel.getValue() != null ? armorModel.getValue()  : value);
            shopArmorRepository.save(shopArmor);
            shop.addItem(shopArmor);
        }
    }
}
