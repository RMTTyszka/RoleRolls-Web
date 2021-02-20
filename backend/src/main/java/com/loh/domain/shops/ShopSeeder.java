package com.loh.domain.shops;

import com.loh.domain.items.equipables.armors.DefaultArmor;
import com.loh.domain.items.equipables.armors.DefaultArmors;
import com.loh.domain.items.equipables.armors.models.ArmorModel;
import com.loh.domain.items.equipables.armors.models.ArmorModelRepository;
import com.loh.domain.items.equipables.weapons.DefaultWeapons;
import com.loh.domain.items.equipables.weapons.models.WeaponModel;
import com.loh.domain.items.equipables.weapons.models.WeaponModelRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class ShopSeeder {

    @Autowired
    ShopRepository shopRepository;
    @Autowired
    ArmorModelRepository armorModelRepository;
    @Autowired
    WeaponModelRepository weaponModelRepository;
    @Autowired
    ShopArmorRepository shopArmorRepository;
    @Autowired
    ShopWeaponRepository shopWeaponRepository;

    public void seed() {
        Shop shop = shopRepository.findBySystemDefaultAndName(true, ShopTokens.HeroCreationShopName);
        if (shop == null) {
            shop = new Shop();
            shop.setName(ShopTokens.HeroCreationShopName);
            shop.setSystemDefault(true);
        }
        for (DefaultArmor armorName: DefaultArmors.lightArmors) {
            AddArmor(shop, armorName.name, 10);
        }
        for (DefaultArmor armorName: DefaultArmors.mediumArmors) {
            AddArmor(shop, armorName.name, 20);
        }
        for (DefaultArmor armorName: DefaultArmors.heavyArmors) {
            AddArmor(shop, armorName.name, 30);
        }
        for (String armorName: DefaultWeapons.lightWeapons) {
            AddWeapon(shop, armorName, 10);
        }
        for (String armorName: DefaultWeapons.mediumWeapons) {
            AddWeapon(shop, armorName, 20);
        }
        for (String armorName: DefaultWeapons.heavyWeapons) {
            AddWeapon(shop, armorName, 30);
        }
        for (String armorName: DefaultWeapons.lightShields) {
            AddWeapon(shop, armorName, 10);
        }
        for (String armorName: DefaultWeapons.mediumShields) {
            AddWeapon(shop, armorName, 20);
        }
        for (String armorName: DefaultWeapons.heavyShields) {
            AddWeapon(shop, armorName, 30);
        }

        shopRepository.save(shop);
    }

    private void AddArmor(Shop shop, String armorName, int value) {
        if (!shop.getItems().stream().filter(a -> a.getItem().getName().equals("Common " + armorName)).findFirst().isPresent()) {
            ArmorModel armorModel = armorModelRepository.findByNameAndSystemDefaultTrue("Common " + armorName);
            ShopArmor shopArmor = new ShopArmor(armorModel, 33, armorModel.getValue() != null ? armorModel.getValue()  : value, 1);
            shopArmorRepository.save(shopArmor);
            shop.addItem(shopArmor);
        }
    }
    private void AddWeapon(Shop shop, String weaponName, int value) {
        if (!shop.getItems().stream().filter(a -> a.getItem().getName().equals("Common " + weaponName)).findFirst().isPresent()) {
            WeaponModel weaponModel = weaponModelRepository.findByNameAndSystemDefaultTrue("Common " + weaponName);
            ShopWeapon shopWeapon = new ShopWeapon(weaponModel, 33, weaponModel.getValue() != null ? weaponModel.getValue()  : value, 1);
            shopWeaponRepository.save(shopWeapon);
            shop.addItem(shopWeapon);
        }
    }
}
