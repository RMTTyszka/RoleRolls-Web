package com.loh.shops;

import com.loh.items.equipable.armors.DefaultArmors;
import com.loh.items.equipable.armors.armorInstance.ArmorInstance;
import com.loh.items.equipable.armors.armorInstance.ArmorInstanceRepository;
import com.loh.items.equipable.armors.armorModel.ArmorModel;
import com.loh.items.equipable.armors.armorModel.ArmorModelRepository;
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
        if (shop.getArmors().stream().filter(a -> a.getArmor().getName().equals("Common " + armorName)).findFirst() != null) {
            ArmorModel armorModel = armorModelRepository.findByNameAndSystemDefaultTrue("Common " + armorName);
            ArmorInstance armorInstance = new ArmorInstance(armorModel, 1);
            armorInstanceRepository.save(armorInstance);
            ShopArmor shopArmor = new ShopArmor(armorInstance, 33, value);
            shopArmorRepository.save(shopArmor);
            shop.addArmor(shopArmor);
        }
    }
}
